using Microsoft.AspNetCore.Mvc;

namespace FINALEXAM.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
