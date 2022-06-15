using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myPOS.Data.Models;
using myPOS.Models;
using myPOS.Services.User;

namespace myPOS.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService userService;

        public UsersController(UserManager<ApplicationUser> userManager, 
                               SignInManager<ApplicationUser> signInManager,
                               IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel registeringUser)
        {
            var emailInUse = userService.EmailInUse(registeringUser.Email);
            var phoneInUse = userService.PhoneNumberInUse(registeringUser.PhoneNumber);

            if (emailInUse)
            {
                ModelState.AddModelError(string.Empty, "The email you entered is already in use.");
            }

            if (phoneInUse)
            {
                ModelState.AddModelError(string.Empty, "The phone number you entered is already in use.");
            }

            if (registeringUser.Password != registeringUser.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password and Confirm Password do not match.");
            }

            if (!ModelState.IsValid)
            {
                return View(registeringUser);
            }

            var user = new ApplicationUser
            {
                Email = registeringUser.Email,
                UserName = registeringUser.Email.Split("@")[0],
                PhoneNumber = registeringUser.PhoneNumber
            };

            var result = await this.userManager.CreateAsync(user, registeringUser.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View(registeringUser);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel login) 
        {
            var user = await this.userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                return InvalidCredentials(login);
            }

            var passwordIsValid = await this.userManager.CheckPasswordAsync(user, login.Password);

            if (!passwordIsValid)
            {
                return InvalidCredentials(login);
            }

            await this.signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        private IActionResult InvalidCredentials(LoginFormModel login)
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials.");

            return View(login);
        }
    }
}
