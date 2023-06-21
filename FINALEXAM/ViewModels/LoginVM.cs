using System.ComponentModel.DataAnnotations;

namespace FINALEXAM.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MaxLength(50)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
