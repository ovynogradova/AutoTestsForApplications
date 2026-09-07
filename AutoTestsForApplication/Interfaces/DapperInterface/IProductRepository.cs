using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IProductRepository
{
    Task<IEnumerable<ProductDTO>> GetAllAsync();
    Task<ProductDTO> GetByIdAsync(int id);
}