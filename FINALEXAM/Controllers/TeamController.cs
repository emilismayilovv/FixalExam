using FINALEXAM.DAL;
using FINALEXAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDBContext _context;

        public TeamController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<AboutTeam> aboutTeams= _context.AboutTeams.Include(at=>at.AboutPosition).ToList();

            return View(aboutTeams);
        }
    }
}
