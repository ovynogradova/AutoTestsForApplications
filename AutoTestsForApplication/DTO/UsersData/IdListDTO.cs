using System.Text.Json.Serialization;

namespace apitest.DTO.UsersData;

public record IdListDTO(
    [property: JsonPropertyName("data")] IReadOnlyList<DatumDTO> Data
);