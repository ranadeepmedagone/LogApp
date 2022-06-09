using System.Text.Json.Serialization;

namespace logapp.Models;

public record TagType{

    [JsonPropertyName("type_id")]
    public int Id { get; set; }

    [JsonPropertyName("type_name")]

    public string TypeName { get; set; }

     public TagTypeDTO asDto => new TagTypeDTO
    {
        Id = Id,
        TypeName = TypeName,
        


    };


}