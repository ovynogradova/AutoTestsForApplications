using apitest.Interfaces.DapperInterface;
using apitest;
using Microsoft.Data.Sqlite;
using Dapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public class DapperTest
{
    private readonly TestPrecondition precondition =  new();
    
    //[Test]
    //public async Task Initialize()
    //{
    //    var connectionString = "Data Source=marketplace.db";
    //    await using var connection = new SqliteConnection(connectionString);
    //    await connection.OpenAsync();
    //    await DatabaseInitializer.InitializeAsync(connection);
    //}

    [Test]
    public async Task GetAllUser()
    {
        var repo = precondition.Provider.GetService<IUserRepository>();
        var users = await repo.GetAllAsync();
        users.Should().HaveCount(15);
    }
    
    [Test]
    public async Task GetUserId()
    {
        var repo = precondition.Provider.GetService<IUserRepository>();
        var user = await repo.GetByIdAsync(10);
        user.Should().NotBeNull();
    }
    [Test]
    public async Task GetAllAddresses()
    {
        var repo = precondition.Provider.GetService<IAddressRepository>();
        var addresses = await repo.GetAllAddressesAsync();
        addresses.Should().HaveCount(15);
    }

    [Test]
    public async Task GetAddressById()
    {
        var repo = precondition.Provider.GetService<IAddressRepository>();
        var address = await repo.GetAddressByUserId(10);
        address.Should().NotBeNull();
    }
    
    [Test]
    public async Task GetUserByFirstAndLastName()
    {
        var repo1 = precondition.Provider.GetService<IUserRepository>();
        var user = await repo1.GetUserByFirstAndLastName("Елена","Кузнецова");
        
        var repo2 = precondition.Provider.GetService<IAddressRepository>();
        var address = await repo2.GetAddressByUserId(user.ID);
        address.City.Should().Be("Казань");
    }
    
    [Test]
    public async Task GetAllCategories()
    {
        var repo = precondition.Provider.GetService<ICategoryRepository>();
        var categories = await repo.GetAllCategoriesAsync();
        categories.Should().HaveCount(6);
    }
    
    
}