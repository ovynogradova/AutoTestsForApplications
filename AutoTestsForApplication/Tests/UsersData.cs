using apitest.DTO.OrderDTO;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Text.Json;
using apitest.DTO.UsersData;

namespace apitest;

public class UsersData
{
    private IdListDTO user;

    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "UsersData.json");
        string data = File.ReadAllText(path);
        user = JsonSerializer.Deserialize<IdListDTO>(data);
    }

    [Test]
    public void NumberOfUsers()
    {
        user.Data.Count().Should().Be(10);
    }
    
    [Test]
    public void FirstUserNameAliceJohnson()
    {
        user.Data.First().ProfileDto.FullName.Should().Be("Alice Johnson");
    }
    
    
}