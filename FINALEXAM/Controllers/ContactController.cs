using System.Security.Cryptography.X509Certificates;
using FINALEXAM.DAL;
using FINALEXAM.Models;
using Microsoft.AspNetCore.Mvc;

namespace FINALEXAM.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDBContext _context;

        public ContactController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<ContactUs> contactUs = _context.ContactUs.ToList();
            return View(contactUs);
        }
    }
}
