using System.Text.Json.Serialization;
using logapp.DTOs;

namespace logapp.Models;

public record User
{

    [JsonPropertyName("user_id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]

    public string Name { get; set; }


    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("hash_password")]
    public string HashPassword { get; set; }

    [JsonPropertyName("last_login")]

    public DateTimeOffset LastLogin { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [JsonPropertyName("status")]

    public bool Status { get; set; }

    [JsonPropertyName("is_superuser")]

    public bool IsSuperUser { get; set; }

    public UserDTO asDto => new UserDTO
    {
        Id = Id,
        Name = Name,
        Email = Email,
        HashPassword = HashPassword,
        LastLogin = LastLogin,
        CreatedAt = CreatedAt,
        Status = Status,
        IsSuperUser = IsSuperUser


    };
}

