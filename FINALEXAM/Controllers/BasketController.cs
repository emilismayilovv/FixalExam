using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FINALEXAM.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();





            List<BasketItemVM> basketItemsVM = new List<BasketItemVM>();

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) return NotFound();

                List<BasketItem> basketItems = await _context.BasketItems
                    .Where(b => b.AppUserId == user.Id && b.OrderId == null)
                    .Include(b => b.HomeProperti)
                    .ToListAsync();

                foreach (BasketItem item in basketItems)
                {
                    basketItemsVM.Add(new BasketItemVM
                    {
                        Count = item.Count,
                        Price = item.Price,
                        Image = item.HomeProperti.Image,
                        Name = item.HomeProperti.Title
                    });
                }

            }
            else
            {
                if (Request.Cookies["Basket"] != null)
                {
                    List<BasketCookiesItemVM> basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
                    for (int i = 0; i < basket.Count; i++)
                    {
                        HomeProperti homeProperti = await _context.HomeProperties
                               .FirstOrDefaultAsync(p => p.Id == basket[i].Id);

                        if (homeProperti != null)
                        {
                            basketItemVMs.Add(new BasketItemVM
                            {
                                Count = basket[i].Count,
                                Name = homeProperti.Title,
                                Price = homeProperti.Price,
                                Image = homeProperti.Image,




                            });

                        }
                        else
                        {
                            basket.Remove(basket[i]);
                        }

                    }
                }
            }
            return View(basketItemVMs);
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            HomeProperti homeProperti = await _context.HomeProperties.FirstOrDefaultAsync(p => p.Id == id);
            if (homeProperti == null) return NotFound();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) return NotFound();

                BasketItem existed = await _context.BasketItems.FirstOrDefaultAsync(b => b.HomePropertiId == id && b.AppUserId == user.Id && b.OrderId == null);
                if (existed != null)
                {
                    existed.Count++;
                }
                else
                {
                    existed = new BasketItem
                    {
                        AppUserId = user.Id,
                        HomePropertiId = homeProperti.Id,
                        Price = homeProperti.Price,
                        Count = 1
                    };
                    await _context.BasketItems.AddAsync(existed);
                }
                await _context.SaveChangesAsync();
            }
            else
            {

                List<BasketCookiesItemVM> basket;

                if (Request.Cookies["Basket"] == null)
                {
                    basket = new List<BasketCookiesItemVM>();
                    basket.Add(new BasketCookiesItemVM
                    {
                        Id = homeProperti.Id,
                        Count = 1
                    });

                }
                else
                {
                    basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
                    BasketCookiesItemVM existed = basket.FirstOrDefault(b => b.Id == homeProperti.Id);

                    if (existed != null)
                    {
                        existed.Count++;
                    }
                    else
                    {
                        basket.Add(new BasketCookiesItemVM
                        {
                            Id = homeProperti.Id,
                            Count = 1
                        });
                    }


                }


                string json = JsonConvert.SerializeObject(basket);

                Response.Cookies.Append("Basket", json);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetBasket()
        {
            List<BasketCookiesItemVM> baskets = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
            return Json(baskets);
        }
    }
}
