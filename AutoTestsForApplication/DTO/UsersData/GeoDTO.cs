using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace apitest.DTO.UsersData;
public record GeoDTO(
    [property: JsonPropertyName("lat")] double Lat,
    [property: JsonPropertyName("lng")] double Lng
);
