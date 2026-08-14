using System.Text.Json.Serialization;
namespace apitest.DTO;

public class UserResponceDTO
{
    [JsonPropertyName("data")]
    public UserDataDTO Data { get; set; }
}