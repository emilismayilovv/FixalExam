using Microsoft.AspNetCore.Mvc;

namespace FINALEXAM.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
