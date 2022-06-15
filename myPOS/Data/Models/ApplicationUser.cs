using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace myPOS.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Credits { get; set; }
    }
}
