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
    
    [Test]
    public void AllIdsAreUnique()
    {
        var ids = user.Data.Select(u => u.Id).ToList(); 
        ids.Should().OnlyHaveUniqueItems(); 
    }
    
    [Test]
    public void HasAtLeastOnePremiumUser()
    {
        bool hasPremiumUser = user.Data.Any(u => u.ProfileDto.Tags.Contains("premium"));
        hasPremiumUser.Should().BeTrue(); 
    }

    [Test]
    public void AllUsersHaveNonEmptyCity()
    {
        var cities = user.Data.Select(u => u.ProfileDto.AddressDto.City).ToList();
        cities.Should().OnlyContain(city => !string.IsNullOrWhiteSpace(city));
    }
    
    [Test]
    public void HasAtLeastOneUserFromStockholm()
    {
        bool hasStockholmUser = user.Data.Any(u => u.ProfileDto.AddressDto.City == "Stockholm");
        hasStockholmUser.Should().BeTrue();
    }
    
    [Test]
    public void AllUsersAgeInRange()
    {
        var ages = user.Data.Select(u => u.ProfileDto.Age).ToList();
        ages.Should().OnlyContain(age => age >= 18 && age <= 60); 
    }
    
    [Test]
    public void HasAtLeastOneAdminUser()
    {
        bool hasAdminUser = user.Data.Any(u => u.Roles.Contains("admin"));
        hasAdminUser.Should().BeTrue(); 
    }
    
}

