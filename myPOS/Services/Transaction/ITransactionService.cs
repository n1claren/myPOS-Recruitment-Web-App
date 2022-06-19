using myPOS.Models;

namespace myPOS.Services.Transactions
{
    public interface ITransactionsService
    {
        public void CompleteTransaction(string senderId, string receiverPhone, int creditsAmount, string message);

        public ICollection<TransactionDTO> UserSentTransactions(string userId);

        public ICollection<TransactionDTO> UserReceivedTransactions(string userId);

        public TransactionsViewModel GetAllTransactions();
    }
}
