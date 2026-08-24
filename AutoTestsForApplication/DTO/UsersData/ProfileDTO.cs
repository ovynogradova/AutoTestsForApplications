using System.Text.Json.Serialization;

namespace apitest.DTO.UsersData;

public record ProfileDTO(
    [property: JsonPropertyName("fullName")] string FullName,
    [property: JsonPropertyName("age")] int Age,
    [property: JsonPropertyName("address")] AddressDTO AddressDto,
    [property: JsonPropertyName("tags")] IReadOnlyList<string> Tags
);