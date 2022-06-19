using System.ComponentModel.DataAnnotations;

namespace myPOS.Models
{
    public class RegisterFormModel
    {
        public string Email { get; init; }

        public string Password { get; init; }

        [Display(Name = "Re-Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; init; }
    }
}
