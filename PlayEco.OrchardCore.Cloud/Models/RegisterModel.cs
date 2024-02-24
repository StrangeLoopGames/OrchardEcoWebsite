using System.ComponentModel.DataAnnotations;

namespace PlayEco.OrchardCore.Cloud.Models;

public class RegisterModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public bool AgeConfirmation { get; set; }
    public bool Newsletter { get; set; }
}