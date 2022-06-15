using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myPOS.Models;
using myPOS.Services.Transactions;
using myPOS.Services.User;
using System.Security.Claims;

namespace myPOS.Controllers
{
    public class CreditsController : Controller
    {
        private readonly IUserService userService;
        private readonly ITransactionsService transactionsService;

        public CreditsController(IUserService userService, ITransactionsService transactionsService)
        {
            this.userService = userService;
            this.transactionsService = transactionsService;
        }

        [Authorize]
        public IActionResult Balance()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var credits = this.userService.GetCreditBalance(userId);
            var username = this.userService.GetUsername(userId);

            ViewBag.Credits = credits;
            ViewBag.Username = username;

            return View();
        }

        [Authorize]
        public IActionResult Send() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Send(SendFormModel send)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userCredits = this.userService.GetCreditBalance(userId);

            if (send.CreditsAmount > userCredits)
            {
                ModelState.AddModelError(string.Empty, "Insufficient credits!");
            }

            if (send.CreditsAmount <= 0)
            {
                ModelState.AddModelError(string.Empty, "You can't send 0 or negative amount credits!");
            }

            var recipientExists = this.userService.PhoneNumberInUse(send.RecipientPhone);

            if (!recipientExists)
            {
                ModelState.AddModelError(string.Empty, "There is no user with this phone number.");
            }

            if (!ModelState.IsValid)
            {
                return View(send);
            }

            this.transactionsService.CompleteTransaction(userId, send.RecipientPhone, send.CreditsAmount, send.Message);

            return RedirectToAction("Credits", "Dashboard");
        }
    }
}
