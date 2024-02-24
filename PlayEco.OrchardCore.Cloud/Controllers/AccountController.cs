using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlayEco.OrchardCore.Cloud.Models;
using PlayEco.OrchardCore.Cloud.Services;

namespace PlayEco.OrchardCore.Cloud.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManagementService _authenicationClient = new();

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Account()
        {
            var user = await _authenicationClient.GetUser();
            return user.success ? this.View("Account", user.userAccount) : this.RedirectToAction("Login");
        }
        [Route("[controller]/[action]")]
        public IActionResult Login() => this.View("Login");
        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            return this.View("Account");
        }
        /// <summary>Attempts to validate the login details provided with Strange Cloud.</summary>
        /// <param name="request"><see cref="LoginViewModel"/> contains login details provided by the user.</param>
        [HttpPost]
        [Route("[controller]/[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> SubmitLoginSlg(LoginViewModel request)
        {
            if (!this.ModelState.IsValid) return this.View("login");
            try
            {
                var (success, userAccount) = await _authenicationClient.AuthenticateUserSlg(request);
                if (!success)
                {
                    this.ModelState.AddModelError("login", "Error Logging In");
                    return this.RedirectToAction("Login", request); 
                } 
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(userAccount.StrangeUser));

                return this.View("Account", userAccount.StrangeUser);
            }
            catch (Exception ex) 
            {                     
                this.ModelState.AddModelError("login", ex.Message);
                return this.RedirectToAction("Login"); 
            }
        }
        
        /// <summary>Register a new user to Strange Cloud.</summary>
        /// <param name="request"><see cref="RegisterModel"/> contains registration details provided by the user.</param>
        [HttpPost]
        [Route("[controller]/[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> SubmitRegister(RegisterModel request)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var (success, message) = await _authenicationClient.RegisterUserSlg(request);
                    if (!success)
                    {
                        this.ModelState.AddModelError("register", message);
                        return this.RedirectToAction("Register", request); 
                    } 
                    var user = await _authenicationClient.GetUser();
                    return this.View("Account", user.userAccount);
                }
                catch (Exception ex) 
                {                     
                    this.ModelState.AddModelError("register", ex.Message);
                    return this.RedirectToAction("Register", request); 
                }   
            }
            return this.View("Login");
        }
        

        [HttpGet]
        public IActionResult Register() => this.View("Register");
        
    }
}
