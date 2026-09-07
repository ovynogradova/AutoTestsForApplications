using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterface;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class ProductRepository : IProductRepository
{
    private readonly string connectionString;

    public ProductRepository(string connection)
    {
        connectionString = connection;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var products = await db.QueryAsync<ProductDTO>("SELECT * FROM Products");
        return products;
    }

    public async Task<ProductDTO> GetByIdAsync(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var product = await db.QueryFirstOrDefaultAsync<ProductDTO>("SELECT * FROM Products WHERE Id = @id", new { id });
        return product;
    }
}