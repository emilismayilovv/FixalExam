using FINALEXAM.DAL;
using FINALEXAM.Models;
using FINALEXAM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDBContext _context;

        public AboutController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<AboutTeam> teamList = _context.AboutTeams.Include(at=>at.AboutPosition).ToList();
            List<HomeOurService> homeOurServices = _context.HomeOurServices.ToList();
            
            AboutVM aboutVM = new AboutVM();
            aboutVM.AboutTeams = teamList;
            aboutVM.OurServices = homeOurServices;

            return View(aboutVM);
        }
    }
}
