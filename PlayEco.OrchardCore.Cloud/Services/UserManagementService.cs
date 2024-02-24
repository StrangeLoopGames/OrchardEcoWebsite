
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
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

        public UserManagementService()
        {
            _httpClient = new HttpClient();
            _authenicationClient = new AuthenticationClient("https://cloud.strangeloopgames.com/", _httpClient);
            _userAccountClient = new UserAccountClient("https://cloud.strangeloopgames.com/", _httpClient);
            _registrationClient = new RegistrationClient("https://cloud.strangeloopgames.com/", _httpClient);
        }
        public async Task<(bool success, AuthenticationResult user)> AuthenticateUserSlg(LoginViewModel request)
        {
            var result = await _authenicationClient.AuthenticateSLGUserAsync(request.Username, request.Password, "1");
            if (result.Token == null) return (false, result);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return (true, result);
        }
        public async Task <(bool success, string token)> RegisterUserSlg(RegisterModel request)
        {
            var result = await _registrationClient.RegisterUserAsync(request.Username, request.Password, request.Email,"");
            if (result.Token == null) return (false, "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return (true, result.Token);
        }
        public async Task<(bool success,StrangeUser? userAccount, string? message)> GetUser()
        {
            try
            {
                var result = await _userAccountClient.GetAccountAsync("");
                return (true, result, "User found successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (false, null, e.Message);
            }

        }
    }
}
