namespace myPOS.Models
{
    public class TransactionsViewModel
    {
        public TransactionsViewModel()
        {
            this.Transactions = new List<TransactionsDTO>();
        }

        public IEnumerable<TransactionsDTO> Transactions { get; set; }
    }
}
