using static src.DTO.InsurancePlanDTO;
using src.DTO;
using src.Entity;

namespace src.Services.InsurancePlan
{
    public interface IInsurancePlan
{
    Task<List<InsurancePlanReadDto>> GetAllInsurancePlansAsync();
    Task<InsurancePlanReadDto> GetInsurancePlanByIdAsync(int id);
    Task<InsurancePlanReadDto> CreateInsurancePlanAsync(InsurancePlanCreateDto createDto);
    Task<bool> UpdateInsurancePlanAsync(int id, InsurancePlanUpdateDto updateDto); 
    Task<bool> DeleteInsurancePlanAsync(int id); 
        Task<List<InsurancePlanReadDto>> SearchInsurancePlansByCoverageType(string coverageType);
}

}
