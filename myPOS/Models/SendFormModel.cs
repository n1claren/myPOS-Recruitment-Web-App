using System.ComponentModel.DataAnnotations;

namespace myPOS.Models
{
    public class SendFormModel
    {
        [Display(Name = "Credits Amount")]
        public int CreditsAmount { get; init; }

        [Display(Name = "Recipient Phone")]
        public string RecipientPhone { get; init; }

        public string Message { get; init; }
    }
}
