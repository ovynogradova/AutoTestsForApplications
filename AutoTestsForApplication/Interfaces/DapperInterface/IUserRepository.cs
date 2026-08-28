using apitest.DTO.DapperDTO;
using Microsoft.ApplicationInsights;

namespace apitest.Interfaces.DapperInterface;

public interface IUserRepository
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<UserDTO> GetByIdAsync(int id);
    Task<UserDTO> GetUserByFirstAndLastName(string firstName, string lastName);
}