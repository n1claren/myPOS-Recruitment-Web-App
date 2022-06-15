using myPOS.Data;
using myPOS.Data.Models;

namespace myPOS.Services.Transactions
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ApplicationDbContext data;

        public TransactionsService(ApplicationDbContext data)
            => this.data = data;

        public void CompleteTransaction(string senderId, string receiverPhone, int creditsAmount, string message)
        {
            var sender = this.data.Users.FirstOrDefault(u => u.Id == senderId);
            var receiver = this.data.Users.FirstOrDefault(u => u.PhoneNumber == receiverPhone);

            var transaction = new Transaction
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                CreditsAmount = creditsAmount,
                Message = message
            };

            sender.Credits -= creditsAmount;
            receiver.Credits += creditsAmount;

            this.data.Transactions.Add(transaction);
            this.data.SaveChanges();
        }
    }
}
