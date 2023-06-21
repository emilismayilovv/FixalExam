using FINALEXAM.DAL;
using FINALEXAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Areas.manage.Controllers
{
    [Area("manage")]
    public class HomeIconController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeIconController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {

            var existed = await _context.HomeIcons.ToListAsync();


            return View(existed);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeIcon homeIcon)
        {

            if (homeIcon == null) return BadRequest();

            await _context.HomeIcons.AddAsync(homeIcon);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {


            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeIcons.FirstOrDefaultAsync(hs => hs.Id == id);
            if (existed == null) NotFound();



            return View(existed);
        }

        [HttpPost]

        public async Task<IActionResult> Update(HomeIcon homeIcon)
        {
            var result = await _context.HomeIcons.FirstOrDefaultAsync(b => b.Id == homeIcon.Id);
            if (result == null) NotFound();


            result.Title = homeIcon.Title;
            result.Number = homeIcon.Number;
            result.Letter = homeIcon.Letter;
            result.Plus = homeIcon.Plus;
            result.MainIcon=homeIcon.MainIcon;



            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeIcons.FirstOrDefaultAsync(blog => blog.Id == id);
            if (existed == null) NotFound();


            _context.HomeIcons.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
