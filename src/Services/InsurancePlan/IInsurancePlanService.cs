using static src.DTO.InsurancePlanDTO;


namespace src.Services.InsurancePlan
{
    public interface IInsurancePlan
    {

        Task<List<InsurancePlanReadDto>> GetAllInsurancePlansAsync();

        Task<InsurancePlanReadDto> GetInsurancePlanByIdAsync(int id);
    }
}
