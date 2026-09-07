using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
}
