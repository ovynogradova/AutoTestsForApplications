using System.Text.Json.Serialization;

namespace apitest.DTO.OrderDTO;

public record CustomerDTO(
    [property: JsonPropertyName("id")]
    int Id,
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("email")]
    string Email,
    [property: JsonPropertyName("phone")]
    string Phone
    );
