using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlayEco.OrchardCore.Cloud.Models;
using PlayEco.OrchardCore.Cloud.Services;
using StrangeCloud.Service.Client.Contracts;

namespace PlayEco.OrchardCore.Cloud.Controllers
{
    public class AccountController(ILogger<AccountController> logger, IHttpContextAccessor session) : Controller
    {   
        private readonly ILogger<AccountController> _logger = logger;
        private readonly UserManagementService _authenticationClient = new();

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Account()
        {
            var userSession = session.HttpContext?.Session.GetString("User");
            if (userSession != null)
            {
                return this.View("Account", JsonConvert.DeserializeObject<StrangeUser>(userSession)); 
            }

            return this.RedirectToAction("Login");
        }
        [Route("[controller]/[action]")]
        public IActionResult Login() => this.View("Login");
        /// <summary>Attempts to validate the login details provided with Strange Cloud.</summary>
        /// <param name="request"><see cref="LoginViewModel"/> contains login details provided by the user.</param>
        [HttpPost]
        [Route("[controller]/[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> SubmitLoginSlg(LoginViewModel request)
        {
            if (!this.ModelState.IsValid) return this.View("login");
            var userSession = session.HttpContext?.Session.GetString("User");
            if(userSession != null)
            {
                var userToString = JsonConvert.DeserializeObject<StrangeUser>(userSession);
                return this.RedirectToAction("Account", userToString);
            }
            try
            {
                var (success, userAccount) = await _authenticationClient.AuthenticateUserSlg(request);
                if (!success)
                {
                    this.ModelState.AddModelError("login", "Error Logging In");
                    return this.RedirectToAction("Login", request); 
                } 
                var userToString = JsonConvert.SerializeObject(userAccount.StrangeUser);
                session.HttpContext.Session.SetString("User", userToString);
                session.HttpContext.Session.SetString("Token", userAccount.Token);
                return this.RedirectToAction("Account", userAccount.StrangeUser);
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
            if (!this.ModelState.IsValid) return this.View("Register");
            var userSession = session.HttpContext?.Session.GetString("User");
            if(userSession != null)
            {
                var userToString = JsonConvert.DeserializeObject<StrangeUser>(userSession);
                return this.RedirectToAction("Account", userToString);
            }
            try
            {
                var (success, user) = await _authenticationClient.RegisterUserSlg(request);
                if (!success)
                {
                    this.ModelState.AddModelError("register", "error registering user");
                    return this.RedirectToAction("Register", request); 
                } 
                var userToString = JsonConvert.SerializeObject(user.StrangeUser);
                session.HttpContext.Session.SetString("User", userToString);
                session.HttpContext.Session.SetString("Token", user.Token);
                return this.RedirectToAction("Account", user.StrangeUser);
            }
            catch (Exception ex) 
            {                     
                this.ModelState.AddModelError("register", ex.Message);
                return this.RedirectToAction("Register", request); 
            }
            return this.View("Login");
        }
        
        [HttpGet]
        [Route ("[controller]/[action]")]
        public IActionResult Forgot() => this.View("Forgot");
        
        [HttpPost]
        [Route("[controller]/[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Forgot(ForgotModel request)
        {
            if (!this.ModelState.IsValid) return this.View("Forgot");
            try
            {
                var success = await _authenticationClient.RequestReset(request);
                this.ModelState.AddModelError("register", "Error requesting reset.");
                return this.View("Forgot");
            }
            catch (Exception ex) 
            {                     
                this.ModelState.AddModelError("register", ex.Message);
                return this.RedirectToAction("Register", request); 
            }
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        [AllowAnonymous]
        public Task <IActionResult> Reset(ResetModel request)
        {
            if (!this.ModelState.IsValid) return Task.FromResult<IActionResult>(this.View("Forgot"));
            var token = request.Token;
            return Task.FromResult<IActionResult>(this.View("Reset", request));
        }
        
        [HttpPost]
        [Route("[controller]/[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DoReset(ResetModel request)
        {
            if (!this.ModelState.IsValid) return this.View("Forgot");
            try
            {
                var success = await _authenticationClient.ResetPassword(request);
                this.ModelState.AddModelError("register", "Error requesting reset.");
                if (success)
                {
                    return this.View("Login");
                }

                return this.View("Forgot");
            }
            catch (Exception ex) 
            {                     
                this.ModelState.AddModelError("register", ex.Message);
                return this.RedirectToAction("Forgot"); 
            }
        }
        
        [HttpGet]
        public IActionResult Register() => this.View("Register");
        
        [HttpGet]
        [Route ("[controller]/[action]")]
        public IActionResult Logout()
        {
            session.HttpContext.Session.Clear();
            return this.RedirectToAction("Login");
        }
        
    }
}
