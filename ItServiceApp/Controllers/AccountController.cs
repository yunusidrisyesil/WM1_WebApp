using ItServiceApp.Extensions;
using ItServiceApp.Models;
using ItServiceApp.Models.Identity;
using ItServiceApp.Services;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ItServiceApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSender _emailSender;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            checkRoles();
            _emailSender = emailSender;
        }

        private void checkRoles()
        {
            foreach (var rolName in RoleModels.Roles)
            {
                if (!_roleManager.RoleExistsAsync(rolName).Result)
                {
                    var result = _roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = rolName,
                    }).Result;
                }
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.UserName), "This username is already claimed");
                return View(model);
            }

            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null)
            {
                ModelState.AddModelError(nameof(model.Email), "This email is already claimed");
                return View(model);
            }

            user = new ApplicationUser
            {
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var count = _userManager.Users.Count();
                await _userManager.AddToRoleAsync(user, count == 1 ? RoleModels.Admin : RoleModels.Passive);

                //Email confirmation
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                var emailMessage = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here</a>",
                    Subject = "Confirm Your Email"
                };
                await _emailSender.SendAsync(emailMessage);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error ocuured");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

            if (result.Succeeded)
            {
                //var user = await _userManager.FindByNameAsync(model.UserName);

                //await _emailSender.SendAsync(new EmailMessage()
                //{
                //    Contacts = new string[] { "yunusyesil85@gmail.com" },
                //    Subject = $"{user.UserName} - User Login",
                //    Body = $"{user.Name} {user.Surname} named user {DateTime.Now:g} logined"
                //});
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Username or password is wrong");
                return View(model);
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            ViewBag.StatueMessage = result.Succeeded ? "Thank you for confirming your email." : "Error conforming your email.";

            if (result.Succeeded && _userManager.IsInRoleAsync(user, RoleModels.Passive).Result)
            {
                await _userManager.RemoveFromRoleAsync(user, RoleModels.Passive);
                await _userManager.AddToRoleAsync(user, RoleModels.User);
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            var model = new UserProfileViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(UserProfileViewModel model)
        {

            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            user.Name = model.Name;
            user.Surname = model.Surname;
            if (user.Email != model.Email)
            {
                await _userManager.RemoveFromRoleAsync(user, RoleModels.User);
                await _userManager.AddToRoleAsync(user, RoleModels.Passive);
                user.Email = model.Email;
                user.EmailConfirmed = false;
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                var emailMessage = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here</a>",
                    Subject = "Confirm Your Email"
                };
                await _emailSender.SendAsync(emailMessage);


            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, ModelState.ToFullErrorString());
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult UpdatePassword()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(PasswordChangeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                ViewBag.Message = "Password updated succesfuly";
                return RedirectToAction(nameof(Logout));
            }
            else
            {
                ViewBag.Message = $"An error occured : {ModelState.ToFullErrorString()}";
            }

            return RedirectToAction(nameof(Profile));
        }

        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                ViewBag.Message = "Email not found";
            }
            else
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmResetPassword", "Account", new { userID = user.Id, code = code }, protocol: Request.Scheme);
                var emailMessage = new EmailMessage()
                {
                    Contacts = new[] { user.Email },
                    Body = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here </a>",
                    Subject = "Reset Password"
                };
                await _emailSender.SendAsync(emailMessage);
                ViewBag.Message = "Password change mail sent to your email";
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult ConfirmResetPassword(string userID,string code)
        {
            if(string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(code))
            {
                return BadRequest("Error");
            }

            ViewBag.Code = code;
            ViewBag.UserID = userID;

            return View();
        }

        [AllowAnonymous,HttpPost]
        public async Task<IActionResult> ConfirmResetPasswordAsync(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserID);

            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View();
            }

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));

            var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);

            if (result.Succeeded)
            {
                //Send email for password change
                TempData["Message"] = "Password changed successfully";
                return View();
            }
            else
            {
                var message = string.Join("<br>", result.Errors.Select(x => x.Description));
                TempData["Message"] = message;
                return View();
            }
        }


    }

}
