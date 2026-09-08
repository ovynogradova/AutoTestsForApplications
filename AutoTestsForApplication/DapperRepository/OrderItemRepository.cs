using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterface;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly string connectionString;

    public OrderItemRepository(string connection)
    {
        connectionString = connection;
    }

    public async Task<IEnumerable<OrderItemDTO>> GetItemsByOrderIdAsync(int orderId)
    {
        using var db = new SqliteConnection(connectionString);
        var items = await db.QueryAsync<OrderItemDTO>("SELECT * FROM OrderItems WHERE OrderId = @orderId", new { orderId });
        return items;
    }
}