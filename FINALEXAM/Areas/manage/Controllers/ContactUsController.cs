using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Areas.manage.Controllers
{
    [Area("manage")]
    public class ContactUsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactUsController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page)
        {
            ViewBag.Page = page;
            ViewBag.Total = Math.Ceiling((decimal)_context.HomeProperties.Count() / 6);
            var existed = await _context.ContactUs.Skip(page * 6).Take(6).ToListAsync();


            return View(existed);
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactUs contactUs)
        {

            if (contactUs == null) return BadRequest();

            if (!contactUs.Photo.CheckSize(500))
            {
                ModelState.AddModelError("Photo", "Olcu boyukdur");
                return View();
            }
            if (!contactUs.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "tip sehvdir");
                return View();
            }

            contactUs.Image = await contactUs.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            await _context.ContactUs.AddAsync(contactUs);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(int? id)
        {


            if (id == null || id < 1) return BadRequest();

            var existed = await _context.ContactUs.FirstOrDefaultAsync(hs => hs.Id == id);
            if (existed == null) NotFound();



            return View(existed);
        }

        [HttpPost]

        public async Task<IActionResult> Update(ContactUs contactUs)
        {
            var result = await _context.ContactUs.FirstOrDefaultAsync(b => b.Id == contactUs.Id);
            if (result == null) NotFound();

            if (contactUs.Photo != null)
            {
                if (!contactUs.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Olcu boyukdur");
                    return View();
                }
                if (!contactUs.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "tip sehvdir");
                    return View();
                }

                result.Image.DeleteFile(_env.WebRootPath, "assets/img");
                contactUs.Image = await contactUs.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");

            }
            result.Title = contactUs.Title;
            result.SubTitle1 = contactUs.SubTitle1;
            result.SubTitle2 = contactUs.SubTitle2;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var existed = await _context.ContactUs.FirstOrDefaultAsync(blog => blog.Id == id);
            if (existed == null) NotFound();

            existed.Image.DeleteFile(_env.WebRootPath, "assets/img");
            _context.ContactUs.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
