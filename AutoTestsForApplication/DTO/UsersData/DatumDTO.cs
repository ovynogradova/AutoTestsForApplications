using System.Text.Json.Serialization;

namespace apitest.DTO.UsersData;

public record DatumDTO(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("profile")] ProfileDTO ProfileDto,
    [property: JsonPropertyName("roles")] IReadOnlyList<string> Roles
);