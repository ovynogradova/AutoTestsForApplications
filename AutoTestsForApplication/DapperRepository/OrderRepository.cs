using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterface;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class OrderRepository : IOrderRepository
{
    private readonly string connectionString;

    public OrderRepository(string connection)
    {
        connectionString = connection;
    }

    public async Task<IEnumerable<OrderDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var orders = await db.QueryAsync<OrderDTO>("SELECT * FROM Orders");
        return orders;
    }

    public async Task<OrderDTO> GetOrderByUserIdAsync(int userId)
    {
        using var db = new SqliteConnection(connectionString);
        var order = await db.QueryFirstOrDefaultAsync<OrderDTO>("SELECT * FROM Orders WHERE UserId = @userId", new { userId });
        return order;
    }
}