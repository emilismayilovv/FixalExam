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
    public class AboutTeamController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutTeamController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page)
        {
            ViewBag.Page = page;
            ViewBag.Total = Math.Ceiling((decimal)_context.HomeProperties.Count() / 6);
            var existed = await _context.AboutTeams.Include(b => b.AboutPosition).Skip(page * 6).Take(6).ToListAsync();


            return View(existed);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.AboutPositions = await _context.AboutPositions.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutTeam aboutTeam)
        {
            ViewBag.AboutPositions = await _context.AboutPositions.ToListAsync();

            var existed = await _context.AboutPositions.FirstOrDefaultAsync(p => p.Id == aboutTeam.AboutPositionId);

            if (existed == null)
            {
                ModelState.AddModelError("ProfessionId", "Ixtisas yoxud");
                return View();
            }

            if (aboutTeam == null) return BadRequest();

            if (!aboutTeam.Photo.CheckSize(500))
            {
                ModelState.AddModelError("Photo", "Olcu boyukdur");
                return View();
            }
            if (!aboutTeam.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "tip sehvdir");
                return View();
            }

            aboutTeam.Image = await aboutTeam.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            await _context.AboutTeams.AddAsync(aboutTeam);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.AboutPositions = await _context.AboutPositions.ToListAsync();

            if (id == null || id < 1) return BadRequest();

            var existed = await _context.AboutTeams.FirstOrDefaultAsync(b => b.Id == id);
            if (existed == null) NotFound();



            return View(existed);
        }
        [HttpPost]

        public async Task<IActionResult> Update(AboutTeam aboutTeam)
        {
            ViewBag.AboutPositions = await _context.AboutPositions.ToListAsync();

            var existed = await _context.AboutPositions.FirstOrDefaultAsync(p => p.Id == aboutTeam.AboutPositionId);

            if (existed == null)
            {
                ModelState.AddModelError("ProfessionId", "Ixtisas yoxud");
                return View();
            }


            var result = await _context.AboutTeams.FirstOrDefaultAsync(b => b.Id == aboutTeam.Id);
            if (result == null) NotFound();

            if (aboutTeam.Photo != null)
            {
                if (!aboutTeam.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Olcu boyukdur");
                    return View();
                }
                if (!aboutTeam.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "tip sehvdir");
                    return View();
                }

                result.Image.DeleteFile(_env.WebRootPath, "assets/img");
                aboutTeam.Image = await aboutTeam.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");

            }
            result.Name = aboutTeam.Name;

            result.AboutPositionId = aboutTeam.AboutPositionId;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var existed = await _context.AboutTeams.FirstOrDefaultAsync(blog => blog.Id == id);
            if (existed == null) NotFound();

            existed.Image.DeleteFile(_env.WebRootPath, "assets/img");
            _context.AboutTeams.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
