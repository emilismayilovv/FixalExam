using System.ComponentModel.DataAnnotations;

namespace FINALEXAM.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string userId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
