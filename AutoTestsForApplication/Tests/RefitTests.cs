using System.Net;
using apitest.DTO;
using apitest.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace apitest;

public class RefitTests
{
    private IUserApiClient _client;
    
    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddRefitClient<IUserApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://reqres.in/api"));
        var provider = services.BuildServiceProvider();
        _client = provider.GetRequiredService<IUserApiClient>();
    }

    [Test]
    public async Task Test1()
    {
        var result = await _client.GetUserAsync(2);
        Assert.Multiple(() =>
        {
            Assert.That(result.Data.Id, Is.EqualTo(2));
            Assert.That(result.Data.Email, Is.Not.Null);  
        });
    }
    
    [Test]
    public async Task Test2()
    {
        var newUser = new CreateUserRequestDTO { Name = "Ivan", Job = "Cook"};
        var responce = await _client.PostUserAsync(newUser);
        Assert.That(responce.Name, Is.EqualTo("Ivan"));

    }

    [Test]
    public async Task Test3()
    {
        var updateUser = new CreateUserRequestDTO { Name = "Ivan", Job = "Driver"};
        var response = await _client.PutUserAsync(2, updateUser);
        Assert.That(response.Job, Is.EqualTo("Driver"));
    }

    [Test]
    public async Task Test4()
    {
        var response = await _client.DeleteUserAsync(2);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}