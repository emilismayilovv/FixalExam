using Microsoft.AspNetCore.Mvc;

namespace FINALEXAM.Areas.manage.Controllers
{
    public class ManageHomeController : Controller
    {
        [Area("manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
