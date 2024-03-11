
using System.Net;
using System.Net.Http.Headers;
using PlayEco.OrchardCore.Cloud.Models;
using StrangeCloud.Service.Client;
using StrangeCloud.Service.Client.Contracts;

namespace PlayEco.OrchardCore.Cloud.Services
{
    public class UserManagementService
    {
        private readonly AuthenticationClient _authenicationClient;
        private readonly UserAccountClient _userAccountClient;
        private readonly HttpClient _httpClient;
        private readonly RegistrationClient _registrationClient;
        private readonly PasswordResetClient _passwordResetClient;
        public UserManagementService()
        {
            _httpClient = new HttpClient();
            _authenicationClient = new AuthenticationClient("https://cloud.strangeloopgames.com/", _httpClient);
            _userAccountClient = new UserAccountClient("https://cloud.strangeloopgames.com/", _httpClient);
            _registrationClient = new RegistrationClient("https://cloud.strangeloopgames.com/", _httpClient);
            _passwordResetClient = new PasswordResetClient("https://cloud.strangeloopgames.com/", _httpClient);
        }
        public async Task<(bool success, AuthenticationResult user)> AuthenticateUserSlg(LoginViewModel request)
        {
            var result = await _authenicationClient.AuthenticateSLGUserAsync(request.Username, request.Password, "1");
            if (result.Token == null) return (false, result);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return (true, result);
        }
        public async Task <(bool success, AuthenticationResult user)> RegisterUserSlg(RegisterModel request)
        {
            var result = await _registrationClient.RegisterUserAsync(request.Username, request.Email, request.Password,"1");
            if (result.Token == null) return (false, null)!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return (true, result);
        }
        public async Task<bool> RequestReset(ForgotModel request)
        {
            var result = await _passwordResetClient.RequestResetAsync(request.Email, "1");
            return result.StatusCode == (int)HttpStatusCode.OK;
        }
        public async Task<bool> ResetPassword(ResetModel request)
        {
            var result = await _passwordResetClient.ResetPasswordAsync(request.Token, request.Password, "1");
                return result.StatusCode == (int)HttpStatusCode.OK;
        }
    }
}
