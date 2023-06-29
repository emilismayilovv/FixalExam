using System.ComponentModel.DataAnnotations.Schema;

namespace FINALEXAM.Models
{
    public class AboutTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public AboutPosition? AboutPosition { get; set; }
        public int? AboutPositionId { get; set; }
        public List<HomeProperti>? HomeProperti { get; set; }
        public List<Order>? Order { get; set; }
        



        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
