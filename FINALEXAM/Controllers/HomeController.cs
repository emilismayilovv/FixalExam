using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<HomeIcon> homeIcons= _context.HomeIcons.ToList();
            List<HomeOurAminity> homeOurAminities=_context.HomeOurAminities.ToList();
            List<HomeOurService> homeOurServices= _context.HomeOurServices.ToList();
            List<HomeSlider> homeSliders= _context.HomeSliders.ToList();
            List<HomeProperti> homePropertis=_context.HomeProperties.Include(hp=>hp.AboutTeam).ToList();

            HomeVM vm=new HomeVM();
            vm.Icons=homeIcons;
            vm.OurAminity=homeOurAminities;
            vm.OurServices=homeOurServices;
            vm.Slider=homeSliders;
            vm.Properti=homePropertis;
            vm.Lastproperty = _context.HomeProperties.OrderByDescending(x => x.Id).FirstOrDefault();

            return View(vm);
        }
    }
}
