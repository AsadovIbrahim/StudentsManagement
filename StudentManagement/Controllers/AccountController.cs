using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Enums;
using StudentManagement.Helpers;
using StudentManagement.Models.Concretes;
using StudentManagement.Services.EmailService.Abstracts;
using StudentManagement.ViewModels;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;


        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMailService mailService)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Role)))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = item.ToString()
                });
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var existingUsername = await _userManager.FindByNameAsync(registerVM.Username);
            if (existingUsername != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(registerVM);
            }

            var existingEmail = await _userManager.FindByEmailAsync(registerVM.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("", "Email already exists.");
                return View(registerVM);
            }

            User user = new User()
            {
                UserName = registerVM.Username,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(user, Role.User.ToString());
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<JsonResult> IsUsernameAvailable(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return Json(user == null);
        }

        [HttpPost]
        public async Task<JsonResult> IsEmailAvailable(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Json(user == null);
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM LoginVM, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(LoginVM);
            }
            User user = new();
            if (LoginVM.Email.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(LoginVM.Email);
            }
            else
            {
                user = await _userManager.FindByNameAsync(LoginVM.Email);

            }
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username Or Password!");
                return View();
            }


            var result = await _signInManager.CheckPasswordSignInAsync(user, LoginVM.Password, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Username Or Password!");
                return View();
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Please Try Again Later!");
                return View();
            }


            await _signInManager.SignInAsync(user, LoginVM.RememberMe);
            var admin = await _userManager.FindByNameAsync("admin");
            if (admin.Id == user.Id)
            {
                return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });

            }
            else return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordVM);
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
            if (user == null)
            {
                return View("Error", "Home");
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, HttpContext.Request.Scheme)!;
            await _mailService.SendMailAsync(new MailRequest()
            {
                Subject = "Reset Password",
                ToEmail = user.Email,
                Body = $"<a href='{link}'>Reset Password</a>"
            });
            Console.WriteLine(link);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string UserId, string token)
        {
            if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(token)) { return BadRequest(); }
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null) { return View("Error", "Home"); };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM, string UserId, string token)
        {
            if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(token)) { return BadRequest(); }
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null) { return View("Error", "Home"); }
            var identityuser = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.ConfirmPassword);
            return RedirectToAction(nameof(Login));
        }
    }
}
