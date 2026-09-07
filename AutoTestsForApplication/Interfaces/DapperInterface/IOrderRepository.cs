using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDTO>> GetAllAsync();
    Task<OrderDTO> GetOrderByUserIdAsync(int userId);
}