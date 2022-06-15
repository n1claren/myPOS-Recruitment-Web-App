namespace myPOS.Services.User
{
    public interface IUserService
    {
        public bool PhoneNumberInUse(string phoneNumber);

        public bool EmailInUse(string email);
    }
}
