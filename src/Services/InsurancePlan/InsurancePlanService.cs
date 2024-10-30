using src.Repository;
using AutoMapper;
using src.Entity;
using static src.DTO.InsurancePlanDTO;

namespace src.Services.InsurancePlan{

public class InsurancePlanService : IInsurancePlan
{
    private readonly InsurancePlanRepository _planRepo;

    protected readonly IMapper _mapper;

    public InsurancePlanService(InsurancePlanRepository planRepo , IMapper mapper)
    {
        _planRepo = planRepo;
        _mapper = mapper;
    }

    // Fetch all insurance plans
    public async Task<List<InsurancePlanReadDto>> GetAllInsurancePlansAsync()
    {
        var planList = await _planRepo.GetAllAsync();
            return _mapper.Map<List<Entity.InsurancePlan>, List<InsurancePlanReadDto>>(planList);
    }

    // Fetch single insurance plan by Id
    public async Task<InsurancePlanReadDto> GetInsurancePlanByIdAsync(Guid id)
    {
        var planFound = await _planRepo.GetByIdAsync(id);
            if (planFound == null)
            {
                // Handle not found case (could throw an exception or return null)
                return null; // or throw new NotFoundException("Gym not found.");
            }
            return _mapper.Map<Entity.InsurancePlan, InsurancePlanReadDto>(planFound);
    }

}
}
