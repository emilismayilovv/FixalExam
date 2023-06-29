using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FINALEXAM.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsRememberMe { get; set; }
        public bool IsSeller { get; set; }
        public string? Image { get; set; }
        public List<Order> Order { get; set; }
        
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
