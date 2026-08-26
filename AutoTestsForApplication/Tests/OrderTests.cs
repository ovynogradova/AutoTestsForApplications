using apitest.DTO.OrderDTO;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Text.Json;

namespace apitest;
public class OrderTests
{
    private OrderDataDTO order;
    
    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "OrderData.json");
        string data = File.ReadAllText(path);
        order = JsonSerializer.Deserialize<OrderDataDTO>(data);
    }

    [Test]
    public void Test1()
    {
        foreach (var item in order.Items)
        {
            TestContext.WriteLine($"{item.ProductId} | {item.Quantity} | {item.Price}");
        }
        order.Items.Should().NotBeNullOrEmpty();
        order.Items.Should().HaveCount(3);
    }

    [Test]
    public void Test2()
    {
        var sum = order.Items.Select(x => x.Quantity * x.Price).Sum();
        var expectedSum = order.Summary.ItemsTotal;
        sum.Should().Be(expectedSum);
    }

    [Test]
    public void Test3()
    {
        var listofItems = order.Items.Where(x => x.Category == "Electronics").ToList();
        foreach (var item in listofItems)
        {
            TestContext.WriteLine($"Electronics: {item.Name}");
        }
        listofItems.Should().OnlyContain(x => x.Category == "Electronics");
    }

    [Test]
    public void Test4()
    {
        order.Payment.Status.Should().Be("paid");
    }

    [Test]
    public void Test5()
    {
        var mostExpensiveItem = order.Items.OrderByDescending(x => x.Price).First();
        using (new AssertionScope())
        {
            mostExpensiveItem.Price.Should().Be(129.99m);
            mostExpensiveItem.Name.Should().Be("Wireless Headphones");
        }

    }

    [Test]
    public void Test6()
    {
        var listofItems = order.Items.Where(x => x.Price > 50).ToList();
        foreach (var item in listofItems)
        {
            TestContext.WriteLine($"{item.ProductId} | {item.Quantity} | {item.Price}");
        }
        listofItems.Should().NotBeEmpty();
    }
}