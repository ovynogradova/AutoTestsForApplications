using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterface;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class UserRepository : IUserRepository
{
    private readonly string connectionString;
    
    public UserRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var users = await db.QueryAsync<UserDTO>("SELECT * FROM Users");
        return users;
    }

    public async Task<UserDTO> GetByIdAsync(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var user = await db.QueryFirstOrDefaultAsync<UserDTO>("SELECT * FROM Users  WHERE Id = @id", new { id });
        return user;
    }
    
    public async Task<UserDTO> GetUserByFirstAndLastName(string firstName, string lastName)
    {
        using var db = new SqliteConnection(connectionString);
        var user = await db.QueryFirstOrDefaultAsync<UserDTO>("SELECT * FROM Users  WHERE FirstName = @firstName and " +
                                                              "LastName = @lastName", new { firstName, lastName });
        return user;
    }
}