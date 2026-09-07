using apitest.Interfaces.DapperInterface;
using apitest;
using Microsoft.Data.Sqlite;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
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
    
    [Test]
    public async Task GetProductById()
    {
        var repo = precondition.Provider.GetService<IProductRepository>();
        var product = await repo.GetByIdAsync(4);

        using (new AssertionScope())
        {
            product.Should().NotBeNull();
            product.Name.Should().Be("MacBook Air M3");
            product.Description.Should().Be("Ноутбук Apple");
            product.Price.Should().Be(129990);
            product.Stock.Should().Be(10);
            product.CategoryId.Should().Be(2);
        }
    }
    
    [Test]
    public async Task GetOrderWithItemsByUserId()
    {
        var orderRepo = precondition.Provider.GetRequiredService<IOrderRepository>();
        var order = await orderRepo.GetOrderByUserIdAsync(2);

        var itemRepo = precondition.Provider.GetRequiredService<IOrderItemRepository>();
        var items = (await itemRepo.GetItemsByOrderIdAsync(order.Id)).ToList();

        using (new AssertionScope())
        {
            order.UserId.Should().Be(2);
            order.TotalPrice.Should().Be(24990);

            items.Should().HaveCount(1);
            items.Single().Quantity.Should().Be(1);
            items.Single().UnitPrice.Should().Be(24990);
        }
    }
}