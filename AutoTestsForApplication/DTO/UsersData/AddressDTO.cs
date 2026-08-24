using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace apitest.DTO.UsersData;

public record AddressDTO(
    [property: JsonPropertyName("street")] string Street,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("geo")] GeoDTO Geo
);