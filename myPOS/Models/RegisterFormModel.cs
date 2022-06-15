namespace myPOS.Models
{
    public class RegisterFormModel
    {
        public string Email { get; init; }

        public string Password { get; init; }

        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; init; }
    }
}
