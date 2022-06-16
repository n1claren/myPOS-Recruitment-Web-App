namespace myPOS.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.UserSentTransactions = new List<TransactionDTO>();
            this.UserReceivedTransactions = new List<TransactionDTO>();
        }

        public ICollection<TransactionDTO> UserSentTransactions { get; set; }

        public ICollection<TransactionDTO> UserReceivedTransactions { get; set; }
    }
}
