using src.Repository;
using AutoMapper;
using src.Entity;
using static src.DTO.InsurancePlanDTO;

namespace src.Services.InsurancePlan
{
    public class InsurancePlanService : IInsurancePlan
    {
        private readonly InsurancePlanRepository _planRepo;
        protected readonly IMapper _mapper;

        public InsurancePlanService(InsurancePlanRepository planRepo, IMapper mapper)
        {
            _planRepo = planRepo;
            _mapper = mapper;
        }

        public async Task<List<InsurancePlanReadDto>> GetAllInsurancePlansAsync()
        {
            var planList = await _planRepo.GetAllAsync();
            return _mapper.Map<List<Entity.InsurancePlan>, List<InsurancePlanReadDto>>(planList);
        }

        public async Task<InsurancePlanReadDto> GetInsurancePlanByIdAsync(int id)
        {
            var planFound = await _planRepo.GetByIdAsync(id);
            if (planFound == null)
            {
                return null; 
            }
            return _mapper.Map<Entity.InsurancePlan, InsurancePlanReadDto>(planFound);
        }
        public async Task<List<InsurancePlanReadDto>> SearchInsurancePlansByCoverageType(string coverageType)
        {
            var plansFound = await _planRepo.SearchInsurancePlansByCoverageType(coverageType);

            if (plansFound == null || !plansFound.Any())
            {
                // Return an empty list or handle this as needed
                return new List<InsurancePlanReadDto>();
            }

            return _mapper.Map<List<Entity.InsurancePlan>, List<InsurancePlanReadDto>>(plansFound);
        }



public async Task<InsurancePlanReadDto> CreateInsurancePlanAsync(InsurancePlanCreateDto createDto)
{
    var plan = _mapper.Map<InsurancePlanCreateDto, Entity.InsurancePlan>(createDto);
    await _planRepo.CreateAsync(plan); 
    return _mapper.Map<Entity.InsurancePlan, InsurancePlanReadDto>(plan);
}



       public async Task<bool> UpdateInsurancePlanAsync(int id, InsurancePlanUpdateDto updateDto)
    {
        var plan = await _planRepo.GetByIdAsync(id);
        if (plan == null)
            return false;

        plan.PlanName = updateDto.PlanName;
        plan.MonthlyPremium = updateDto.MonthlyPremium;
        // plan.CoverageType = updateDto.CoverageType;
        plan.PlanDescription = updateDto.planDescription;
        // plan.CoverageDetails = updateDto.CoverageDetails;

        await _planRepo.UpdateAsync(plan); 

        return true;
    }

    public async Task<bool> DeleteInsurancePlanAsync(int id)
    {
        var plan = await _planRepo.GetByIdAsync(id);
        if (plan == null)
            return false;

        await _planRepo.DeleteAsync(plan);
        return true;
    }
    }
}
