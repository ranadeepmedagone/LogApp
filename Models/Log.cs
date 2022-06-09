using System.Text.Json.Serialization;
using logapp.DTOs;

namespace logapp.Models;

public record Log
{

    [JsonPropertyName("log_id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]

    public string Title { get; set; }


    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("stack_trace")]
    public string StackTrace { get; set; }

    

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;



    [JsonPropertyName("updated_by_user_id")]

    public int UdatedByUserId { get; set; }

    [JsonPropertyName("partially_deleted")]

    public bool PartiaLlyDeleted { get; set; }

    public LogDTO asDto => new LogDTO
    {
        Id = Id,
        Title = Title,
        Description = Description,
        StackTrace = StackTrace,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        UdatedByUserId = UdatedByUserId,
        PartiaLlyDeleted = PartiaLlyDeleted


    };
}

