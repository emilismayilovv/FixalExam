using System.Reflection.Metadata;
using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Areas.manage.Controllers
{
    [Area("manage")]
    public class HomeSliderController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeSliderController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {

            var existed = await _context.HomeSliders.ToListAsync();


            return View(existed);
        }


        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeSlider homeSlider)
        {

            if (homeSlider == null) return BadRequest();

            if (!homeSlider.Photo.CheckSize(500))
            {
                ModelState.AddModelError("Photo", "Olcu boyukdur");
                return View();
            }
            if (!homeSlider.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "tip sehvdir");
                return View();
            }

            homeSlider.Image = await homeSlider.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            await _context.HomeSliders.AddAsync(homeSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(int? id)
        {
            

            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeSliders.FirstOrDefaultAsync(hs => hs.Id == id);
            if (existed == null) NotFound();



            return View(existed);
        }

        [HttpPost]

        public async Task<IActionResult> Update(HomeSlider homeSlider)
        {
            var result = await _context.HomeSliders.FirstOrDefaultAsync(b => b.Id == homeSlider.Id);
            if (result == null) NotFound();

            if (homeSlider.Photo != null)
            {
                if (!homeSlider.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Olcu boyukdur");
                    return View();
                }
                if (!homeSlider.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "tip sehvdir");
                    return View();
                }

                result.Image.DeleteFile(_env.WebRootPath, "assets/img");
                homeSlider.Image = await homeSlider.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");

            }
            result.Title = homeSlider.Title;
            result.SubTitle= homeSlider.SubTitle;
            result.Description = homeSlider.Description;
            

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeSliders.FirstOrDefaultAsync(blog => blog.Id == id);
            if (existed == null) NotFound();

            existed.Image.DeleteFile(_env.WebRootPath, "assets/img");
            _context.HomeSliders.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }






    }
}
