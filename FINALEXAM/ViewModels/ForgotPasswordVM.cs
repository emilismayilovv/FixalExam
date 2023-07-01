using System.ComponentModel.DataAnnotations;
 

namespace FINALEXAM.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

    }
}
