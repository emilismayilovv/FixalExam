using System.ComponentModel.DataAnnotations.Schema;

namespace FINALEXAM.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle1 { get; set; }
        public string SubTitle2 { get; set; }
        public string? Image { get; set;}
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
