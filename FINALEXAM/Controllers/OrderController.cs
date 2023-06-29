using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly AppDBContext _context;

        public OrderController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env, AppDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
            _context = context;


        }
        public IActionResult Index()
        {
            return View();
        }



        /*public async Task<IActionResult> Checkout()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Items = await _context.BasketItems.Where(b => b.AppUserId==user.Id&&b.OrderId==null)
                .Include(b=>b.HomeProperti).ToListAsync();
             return View();
        }*/


        [HttpPost]

        public async Task<IActionResult> Checkout(OrderVM orderVM,int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> items=await _context.BasketItems.Where(b => b.AppUserId == user.Id && b.OrderId == null)
                .Include(b => b.HomeProperti).ToListAsync();



            decimal total = 0;
            foreach(var item in items)
            {
                total += item.Count * item.Price;
            }

            Order order = new Order
            {

                Address = orderVM.Address,
                Status=null,
                AppUserId = user.Id,
                PurchasedAt=DateTime.Now,
                TotalPrice=total,
                BasketItems=items,
                AboutTeamId=id,
                
            };

            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

    }
}

