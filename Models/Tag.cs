using System.Text.Json.Serialization;
using logapp.DTOs;

namespace logapp.Models;

public record Tag
{

    [JsonPropertyName("tag_id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]

    public string Name { get; set; }


    [JsonPropertyName("type_id")]

    public int TypeId { get; set; }



    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;






    public TagDTO asDto => new TagDTO
    {
        Id = Id,
        Name = Name,
        TypeId = TypeId,
        CreatedAt = CreatedAt,


    };

    
}

