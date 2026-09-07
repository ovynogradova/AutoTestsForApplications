using apitest.DTO.BookStoreDTO;
using Refit;

namespace apitest.Interfaces.IBookStore;

public interface IBookStore
{
    [Post ("/Account/v1/User")]
    Task <CreateUserResponceDTO> CreateUserAsync([Body] UserDTO user);
    
    [Post ("/Account/v1/GenerateToken")]
    Task<GetTokenDTO> GenerateTokenAsync([Body] LoginRequestDTO login);
}