using System.ComponentModel.DataAnnotations.Schema;

namespace FINALEXAM.Models
{
    public class HomeSlider
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
