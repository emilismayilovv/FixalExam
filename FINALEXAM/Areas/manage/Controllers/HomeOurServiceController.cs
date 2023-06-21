using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Areas.manage.Controllers
{
    [Area("manage")]
    public class HomeOurServiceController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeOurServiceController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {

            var existed = await _context.HomeOurServices.ToListAsync();


            return View(existed);
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeOurService homeOurService)
        {

            if (homeOurService == null) return BadRequest();

            if (!homeOurService.Photo.CheckSize(500))
            {
                ModelState.AddModelError("Photo", "Olcu boyukdur");
                return View();
            }
            if (!homeOurService.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "tip sehvdir");
                return View();
            }

            homeOurService.Image = await homeOurService.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            await _context.HomeOurServices.AddAsync(homeOurService);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(int? id)
        {


            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeOurServices.FirstOrDefaultAsync(hs => hs.Id == id);
            if (existed == null) NotFound();



            return View(existed);
        }

        [HttpPost]

        public async Task<IActionResult> Update(HomeOurService homeOurService)
        {
            var result = await _context.HomeOurServices.FirstOrDefaultAsync(b => b.Id == homeOurService.Id);
            if (result == null) NotFound();

            if (homeOurService.Photo != null)
            {
                if (!homeOurService.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Olcu boyukdur");
                    return View();
                }
                if (!homeOurService.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "tip sehvdir");
                    return View();
                }

                result.Image.DeleteFile(_env.WebRootPath, "assets/img");
                homeOurService.Image = await homeOurService.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");

            }
            result.Title = homeOurService.Title;
            result.Description = homeOurService.Description;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeOurServices.FirstOrDefaultAsync(blog => blog.Id == id);
            if (existed == null) NotFound();

            existed.Image.DeleteFile(_env.WebRootPath, "assets/img");
            _context.HomeOurServices.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }

}
