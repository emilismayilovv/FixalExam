using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Areas.manage.Controllers
{
    [Area("manage")]
    public class HomeOurAminitiesController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeOurAminitiesController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {

            var existed = await _context.HomeOurAminities.ToListAsync();


            return View(existed);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeOurAminity homeOurAminity)
        {

            if (homeOurAminity == null) return BadRequest();

            await _context.HomeOurAminities.AddAsync(homeOurAminity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {


            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeOurAminities.FirstOrDefaultAsync(hs => hs.Id == id);
            if (existed == null) NotFound();



            return View(existed);
        }

        [HttpPost]

        public async Task<IActionResult> Update(HomeOurAminity homeOurAminity)
        {
            var result = await _context.HomeOurAminities.FirstOrDefaultAsync(b => b.Id == homeOurAminity.Id);
            if (result == null) NotFound();

            
            result.Title = homeOurAminity.Title;
            result.Icon = homeOurAminity.Icon;
            


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeOurAminities.FirstOrDefaultAsync(blog => blog.Id == id);
            if (existed == null) NotFound();


            _context.HomeOurAminities.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
