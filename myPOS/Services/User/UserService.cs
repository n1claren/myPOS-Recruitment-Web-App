using myPOS.Data;
using myPOS.Models;

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

        public AdminDashboardViewModel GetUsers()
        {
            var users = this.data
                .Users
                .OrderByDescending(u => u.Credits)
                .Select(u => new UserDTO
                {
                    Username = u.UserName,
                    Credits = u.Credits,
                    TransactionsMade = this.data.Transactions.Where(t => t.SenderId == u.Id).Count(),
                    TransactionsReceived = this.data.Transactions.Where(t => t.ReceiverId == u.Id).Count()
                })
                .ToList();

            var user = users.Where(u => u.Username == "Admin").FirstOrDefault();
            users.Remove(user);

            return new AdminDashboardViewModel
            {
                Users = users
            };
        }
    }
}
