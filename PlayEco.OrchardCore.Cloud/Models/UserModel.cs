using System.ComponentModel.DataAnnotations;

namespace PlayEco.OrchardCore.Cloud.Models;

public class Usermodel
{
    public string? Id { get; set; }
    public string? Username { get; set; }
    public string? AvatarUrl { get; set; }
    public string? AvatarDna { get; set; }
    public int? EcoCredits { get; set; }
    public bool? OwnsEco { get; set; }
    public bool? Verified { get; set; }
    public List<Item>? Items { get; set; }
    public int? SteamId { get; set; }
    public string? TwitchId { get; set; }
    public int? Permissions { get; set; }
    public DateTime? BannedUntil { get; set; }
    public string? BannedReason { get; set; }
    public bool? IsBanned { get; set; }
}

public interface Item
{
    public string? Type { get; set; }
    public int Amount { get; set; }
}
