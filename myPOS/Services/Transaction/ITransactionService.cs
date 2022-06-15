namespace myPOS.Services.Transactions
{
    public interface ITransactionsService
    {
        public void CompleteTransaction(string senderId, string receiverPhone, int creditsAmount, string message);
    }
}
