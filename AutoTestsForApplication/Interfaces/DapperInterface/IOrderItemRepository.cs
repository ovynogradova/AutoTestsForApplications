using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItemDTO>> GetItemsByOrderIdAsync(int orderId);
}