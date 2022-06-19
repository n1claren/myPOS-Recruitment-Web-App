namespace myPOS.Models
{
    public class TransactionsDTO
    {
        public string SenderUsername { get; set; }

        public string ReceiverUsername { get; set; }

        public int Amount { get; set; }

        public string Message { get; set; }
    }
}
