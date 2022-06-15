using myPOS.Data;

namespace myPOS.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data) 
            => this.data = data;

        public bool EmailInUse(string email)
            => this.data.Users.Any(u => u.Email == email);

        public int GetCreditBalance(string userId)
            => this.data.Users.FirstOrDefault(u => u.Id == userId).Credits;

        public string GetUsername(string userId)
            => this.data.Users.FirstOrDefault(u => u.Id == userId).UserName;

        public bool PhoneNumberInUse(string phoneNumber)
            => this.data.Users.Any(u => u.PhoneNumber == phoneNumber);
    }
}
