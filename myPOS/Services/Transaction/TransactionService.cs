using myPOS.Data;
using myPOS.Data.Models;
using myPOS.Models;

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

        public ICollection<TransactionDTO> UserReceivedTransactions(string userId)
            => this.data
            .Transactions
            .Where(t => t.ReceiverId == userId)
            .OrderByDescending(t => t.Id)
            .Select(t => new TransactionDTO
            {
                Nickname = t.Sender.UserName,
                Credits = t.CreditsAmount,
                Message = t.Message
            })
            .ToList();

        public ICollection<TransactionDTO> UserSentTransactions(string userId)
            => this.data
            .Transactions
            .Where(t => t.SenderId == userId)
            .OrderByDescending(t => t.Id)
            .Select(t => new TransactionDTO
            {
                Nickname = t.Receiver.UserName,
                Credits = t.CreditsAmount,
                Message = t.Message
            })
            .ToList();

        public TransactionsViewModel GetAllTransactions()
        {
            var transactions = this.data
                                   .Transactions
                                   .OrderByDescending(t => t.Id)
                                   .Select(t => new TransactionsDTO
                                   {
                                       SenderUsername = t.Sender.UserName,
                                       ReceiverUsername = t.Receiver.UserName,
                                       Amount = t.CreditsAmount,
                                       Message = t.Message
                                   })
                                   .ToList();

            return new TransactionsViewModel
            {
                Transactions = transactions
            };
        }
    }
}
