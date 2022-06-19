namespace myPOS.Models
{
    public class UserDTO
    {
        public string Username { get; set; }

        public int Credits { get; set; }

        public int TransactionsMade { get; set; }

        public int TransactionsReceived { get; set; }
    }
}
