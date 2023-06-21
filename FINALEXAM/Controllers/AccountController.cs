using FINALEXAM.Models;
using FINALEXAM.Utilities.Enums;
using FINALEXAM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FINALEXAM.Utilities.Extensions;
using FINALEXAM.DAL;

namespace FINALEXAM.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly AppDBContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager, IWebHostEnvironment env, AppDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM newUser)
        {
            if (!ModelState.IsValid) return View();

            AppUser member = await _userManager.FindByNameAsync(newUser.Username);
            if (member != null)
            {
                ModelState.AddModelError("Username", "Username already taken");
                return View();
            }

            member = await _userManager.FindByEmailAsync(newUser.Email);
            if (member != null)
            {
                ModelState.AddModelError("Email", "Email already taken");
                return View();
            }

            if(newUser.Photo is null)
            {
                ModelState.AddModelError("Image", "Image can not be null");
                return View();
            }
            if (!newUser.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("File", "File type must be image");
                return View();

            }
            if (!newUser.Photo.CheckSize(2000))
            {
                ModelState.AddModelError("File", "File type must be image");
                return View();

            }

            member = new AppUser
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                UserName = newUser.Name,
                Email = newUser.Email,
                PhoneNumber=newUser.Phone,
                IsSeller = newUser.IsSeller,
                Image = await newUser.Photo.CreateFileAsync(_env.WebRootPath, "assets/img"),

            };
            var result=await _userManager.CreateAsync(member,newUser.Password);
            /*if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }*/

            if (newUser.IsSeller == true)
            {
                await _userManager.AddToRoleAsync(member, "seller");

                AboutTeam team = new AboutTeam
                {
                    Name = newUser.Name,
                    Surname = newUser.Surname,
                    Email = newUser.Email,
                    Phone = newUser.Phone,
                    Image = await newUser.Photo.CreateFileAsync(_env.WebRootPath, "assets/img"),
                    HomeProperti = new List<HomeProperti>()

                };

                await  _context.AboutTeams.AddAsync(team);
                await _context.SaveChangesAsync();
            }
            else
            {
                await _userManager.AddToRoleAsync(member, "member");
            }
            await _signInManager.SignInAsync(member, true);



            return RedirectToAction("Index", "Home"); 

            /*AppUser user = new AppUser
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Surname = newUser.Surname,
                UserName=newUser.Username
            };
            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
                return View();
            }

            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");*/
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Login(LoginVM user,string? ReturnUrl)
        {
            if (!ModelState.IsValid) { return View(); }

            var existed = await _userManager.FindByEmailAsync(user.UsernameOrEmail);
            if (existed == null)
            {
                existed = await _userManager.FindByNameAsync(user.UsernameOrEmail);
                if (existed == null)
                {
                    ModelState.AddModelError("", "Username or Email wrpng");
                    return View();
                }
            }

            var result = await _signInManager.PasswordSignInAsync(existed, user.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username,Email or Password is not correct");
                return View();
            }
            if(ReturnUrl!= null)
            {

                return Redirect(ReturnUrl);

            }

            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> CreateRoles()
        {
            foreach(var item in Enum.GetValues(typeof(UserRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
