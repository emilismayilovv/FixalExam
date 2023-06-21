using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FINALEXAM.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDBContext _context;

        public BasketController(AppDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
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
                            Image=homeProperti.Image,

                            


                        });

                    }
                    else
                    {
                        basket.Remove(basket[i]);
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
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetBasket()
        {
            List<BasketCookiesItemVM> baskets = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
            return Json(baskets);
        }
    }
}
