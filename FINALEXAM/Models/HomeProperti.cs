using System.ComponentModel.DataAnnotations.Schema;

namespace FINALEXAM.Models
{
    public class HomeProperti
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int BedRoomNumbers { get; set; }
        public int BathRoomNumbers { get; set; }
        public int SquareFT { get;set; }
        public string? Image { get; set; }
        public AboutTeam? AboutTeam { get; set; }
        public int? AboutTeamId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
