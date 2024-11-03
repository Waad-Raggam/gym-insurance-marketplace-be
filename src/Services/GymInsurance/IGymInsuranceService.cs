using System.Collections.Generic;
using System.Threading.Tasks;
using src.DTO;
using src.Entity;

namespace src.Repository
{
    public interface IGymInsuranceService
    {
       Task<GymInsuranceReadDto> CreateOnAsync(GymInsuranceCreateDto createDto);

        Task<List<GymInsuranceReadDto>> GetAllAsync();

        Task<GymInsuranceReadDto> GetByIdAsync(Guid id);

        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOnAsync(Guid id, GymInsuranceUpdateDto updateDto);
    }
}
