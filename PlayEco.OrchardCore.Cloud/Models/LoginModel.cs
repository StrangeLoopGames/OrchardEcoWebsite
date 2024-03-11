using System.ComponentModel.DataAnnotations;

namespace PlayEco.OrchardCore.Cloud.Models;

public class LoginViewModel
{
    public string Username { get; set; }

    public string Password { get; set; }
}