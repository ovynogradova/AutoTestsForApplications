using System.Net;
using apitest.DTO.BookStoreDTO;
using apitest.Interfaces.IBookStore;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace apitest;

public class BookStoreTests
{
    private IBookStore API;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services
            .AddRefitClient<IBookStore>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://demoqa.com");
            });

        var provider = services.BuildServiceProvider();
        API = provider.GetRequiredService<IBookStore>();
    }

    [Test]
    public async Task CreateUserAsync()
    {
        var user = new UserDTO{UserName = "Inga3", Password = "StrongPass123!"};
        var response = await API.CreateUserAsync(user);
    }
    
    [Test]
    public async Task GetToken()
    {
        try
        {
            await API.CreateUserAsync(new UserDTO
            {
                UserName = "Inga5", Password = "StrongPass321!"
            });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            // Пользователь уже создан при прошлом запуске теста - это ожидаемо, продолжаем.
        }

        var tokenResponse = await API.GenerateTokenAsync(new LoginRequestDTO
        {
            UserName = "Inga5",
            Password = "StrongPass321!"
        });

        tokenResponse.Token.Should().NotBeNullOrEmpty();
        tokenResponse.Status.Should().Be("Success");
        tokenResponse.Result.Should().Contain("authorized");
    }
}