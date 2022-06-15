using System.ComponentModel.DataAnnotations;

namespace myPOS.Data.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string SenderId { get; init; }
        public ApplicationUser Sender { get; init; }

        [Required]
        public string ReceiverId { get; init; }
        public ApplicationUser Receiver { get; init; }

        [Required]
        public int CreditsAmount { get; init; }

        [MaxLength(300)]
        public string Message { get; init; }
    }
}
