using FINALEXAM.DAL;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FINALEXAM.Models;
using FINALEXAM.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FINALEXAM.Areas.manage.Controllers
{
    [Area("manage")]
    public class HomePropertiController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public HomePropertiController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page)
        {
            ViewBag.Page = page;
            ViewBag.Total = Math.Ceiling((decimal)_context.HomeProperties.Count() / 6);

            var existed = await _context.HomeProperties.Include(b => b.AboutTeam).Skip(page * 6).Take(6).ToListAsync();


            return View(existed);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.AboutTeams = await _context.AboutTeams.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeProperti homeProperti)
        {
            ViewBag.AboutTeams = await _context.AboutTeams.ToListAsync();

            var existed = await _context.AboutTeams.FirstOrDefaultAsync(p => p.Id == homeProperti.AboutTeamId);

            if (existed == null)
            {
                ModelState.AddModelError("AboutTeamId", "Ixtisas yoxud");
                return View();
            }

            if (homeProperti == null) return BadRequest();

            if (!homeProperti.Photo.CheckSize(500))
            {
                ModelState.AddModelError("Photo", "Olcu boyukdur");
                return View();
            }
            if (!homeProperti.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "tip sehvdir");
                return View();
            }

            homeProperti.Image = await homeProperti.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            await _context.HomeProperties.AddAsync(homeProperti);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.AboutTeams = await _context.AboutTeams.ToListAsync();

            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeProperties.FirstOrDefaultAsync(b => b.Id == id);
            if (existed == null) NotFound();



            return View(existed);
        }
        [HttpPost]

        public async Task<IActionResult> Update(HomeProperti homeProperti)
        {
            ViewBag.AboutTeams = await _context.AboutTeams.ToListAsync();

            var existed = await _context.AboutTeams.FirstOrDefaultAsync(p => p.Id == homeProperti.AboutTeamId);

            if (existed == null)
            {
                ModelState.AddModelError("AboutTeamId", "Ixtisas yoxud");
                return View();
            }


            var result = await _context.HomeProperties.FirstOrDefaultAsync(b => b.Id == homeProperti.Id);
            if (result == null) NotFound();

            if (homeProperti.Photo != null)
            {
                if (!homeProperti.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Olcu boyukdur");
                    return View();
                }
                if (!homeProperti.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "tip sehvdir");
                    return View();
                }

                result.Image.DeleteFile(_env.WebRootPath, "assets/img");
                homeProperti.Image = await homeProperti.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");

            }
            result.Title = homeProperti.Title;
            result.Price = homeProperti.Price;
            result.BedRoomNumbers = homeProperti.BedRoomNumbers;
            result.BathRoomNumbers= homeProperti.BathRoomNumbers;
            result.AboutTeamId= homeProperti.AboutTeamId;
            result.Description= homeProperti.Description;
            result.SquareFT= homeProperti.SquareFT;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var existed = await _context.HomeProperties.FirstOrDefaultAsync(blog => blog.Id == id);
            if (existed == null) NotFound();

            existed.Image.DeleteFile(_env.WebRootPath, "assets/img");
            _context.HomeProperties.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
