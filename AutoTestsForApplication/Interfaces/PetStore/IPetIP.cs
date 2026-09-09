using apitest.DTO.PetModelsDTO;
using Refit;

namespace apitest.Interfaces.PetStore;

public interface IPetIP
{
    [Get("/pets")]
    Task<AllPetsResponceDTO> GetAllPetAsync();

    [Get("/pets/{id}")]
    Task<PetDTO> GetPetByIdAsync(string id);
    
    [Get("/pets")]
    Task<AllPetsResponceDTO> GetAllPetByStatusAndLimitAsync([Query]int limit, [Query]string status);
}