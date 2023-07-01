using FINALEXAM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.DAL
{
    public class AppDBContext:IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<HomeOurAminity> HomeOurAminities { get; set; }
        public DbSet<HomeOurService> HomeOurServices { get; set;}
        public DbSet<HomeIcon> HomeIcons { get; set; }
        public DbSet<AboutPosition> AboutPositions { get; set; }
        public DbSet<AboutTeam> AboutTeams { get; set; }
        public DbSet<HomeProperti> HomeProperties { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<HomeType> HomeTypes { get; set; }

    }
}
