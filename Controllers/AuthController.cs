using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using x_sinema.Constans;
using x_sinema.Data;
using x_sinema.Models;
using x_sinema.ViewModels;

namespace x_sinema.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public AuthController(UserManager<UserModel> userManager, 
                              SignInManager<UserModel> signInManager, 
                              RoleManager<IdentityRole> roleManager,
                              ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration() => View(new RegistrationViewModel());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["message_auth"] =  MyFlasher.flasher("This email address is already in use", gagal: true);
                
                return View(registerViewModel);
            }

            var newUser = new UserModel()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.Username,
                Email = registerViewModel.EmailAddress,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.Customer);
                TempData["message_auth"] = MyFlasher.flasher("Congratulations, your account has been registered!", berhasil: true);
                
                return RedirectToAction("Login", "Auth");
            }

            TempData["message_auth"] = MyFlasher.flasher("Oops error was found", gagal: true);

            return View(registerViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        TempData["message"] = MyFlasher.flasher("Login success", berhasil: true);
                        return RedirectToAction("Index", "Movies");
                    }
                }

                TempData["message_auth"] = MyFlasher.flasher("Wrong password", gagal: true);
                return View(loginViewModel);
            }

            TempData["message_auth"] = MyFlasher.flasher("Email address is not registered", gagal: true);
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["message"] = MyFlasher.flasher("Session ended, you Logout <strong>successfully</strong>", berhasil: true);
            return RedirectToAction("Index", "Movies");
        }

    }
}
