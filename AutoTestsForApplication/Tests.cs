using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using apitest.DTO;
namespace apitest;
public class Tests
{
    private static HttpClient client;
    //работает только для тестов в классе Test
    [OneTimeSetUp]
    public void Setup()
    {
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://reqres.in/api/")
        };
        client.DefaultRequestHeaders.Add("x-api-key", "free_user_3HtrfRV1E2a7q2ygfpglr1qMRUV");
    }

    [Test]
    public async Task Test1()
    { 
        //Get запрос
        using HttpResponseMessage response = await client.GetAsync("users/2");
        //проверка статускода
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task Test2()
    {
        using HttpResponseMessage response = await client.GetAsync("users/2");
        string jsonGet = await response.Content.ReadAsStringAsync();
        UserResponceDTO userResponce = JsonSerializer.Deserialize<UserResponceDTO>(jsonGet);
        UserDataDTO user = userResponce.Data;
        if (user.Id == 2)
        {
        
        }
        else
        {
            throw new Exception();
        }
        
    }   
    
    [Test]
    public async Task Test3()
    {
        CreateUserRequestDTO newUser = new CreateUserRequestDTO
        {
            Name = "Ivan",
            Job = "cook"
        };

        using HttpResponseMessage response = await client.PostAsJsonAsync("users", newUser);
        string jsonGet = await response.Content.ReadAsStringAsync();
        CreateUserResponseDTO userResponse = JsonSerializer.Deserialize<CreateUserResponseDTO>(jsonGet);

    }
    
    [Test]
    public async Task Test4()
    {
        CreateUserRequestDTO updatedUser = new CreateUserRequestDTO
        {
            Name = "Ivan",
            Job = "driver"
        };

        using HttpResponseMessage response = await client.PutAsJsonAsync("users/2", updatedUser);
        response.EnsureSuccessStatusCode();
    }
    
    [Test]
    public async Task Test5()
    {
        using HttpResponseMessage response = await client.DeleteAsync("users/2");
        response.EnsureSuccessStatusCode();
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        client.Dispose();
    }
}

//free_user_3HtrfRV1E2a7q2ygfpglr1qMRUV - my token