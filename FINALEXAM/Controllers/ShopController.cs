using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(AppDBContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }
        public async Task<IActionResult> Index(int page,string search, int? order, int?[] hometypeid)
        {
            IQueryable<HomeProperti> query = _context.HomeProperties.Include(hp=>hp.HomeType).Include(hp=>hp.AboutTeam).Skip(page * 6).Take(6).AsQueryable();

            ViewBag.Page = page;
            ViewBag.Total = Math.Ceiling((decimal)_context.HomeProperties.Count() / 6);

            switch (order)
            {
                case 1:
                    query = query.OrderBy(p => p.Title);
                    break;
                case 2:
                    query = query.OrderBy(p => p.Price);
                    break;
                case 3:
                    query = query.OrderByDescending(p => p.Id);
                    break;
            }
            if (!String.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.ToLower().Contains(search.ToLower()));
            }
            if (hometypeid.Length != 0)
            {
                query = query.Where(p => hometypeid.Contains(p.HomeType.Id));
            }
            ShopVM shopVM = new ShopVM
            {
                homeType = await _context.HomeTypes.Include(c => c.homePropertis).ToListAsync(),
                homePropertis = await query.ToListAsync(),
                homeTypeId = hometypeid,
                Order = order,
                Search = search
            };

            return View(shopVM);
        }
    }
}
