using apitest.DTO;
using Refit;

namespace apitest.Interfaces;

[Headers("x-api-key: free_user_3HtrfRV1E2a7q2ygfpglr1qMRUV")]

public interface IUserApiClient
{
    [Get("/users/{id}")]
    Task<UserResponceDTO> GetUserAsync(int id);
    
    [Post("/users")]
    Task<CreateUserRequestDTO>PostUserAsync([Body] CreateUserRequestDTO user);
    
    [Put("/users/{id}")]
    Task<CreateUserRequestDTO>PutUserAsync(int id, [Body] CreateUserRequestDTO user);
    
    [Delete("/users/{id}")]
    Task<ApiResponse<string>> DeleteUserAsync(int id);
}