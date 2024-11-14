using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.DTO;
using src.Entity;
using src.Repository;

namespace src.Services
{
    public class GymInsuranceService : IGymInsuranceService
    {
        private readonly GymInsuranceRepository _gymInsuranceRepo;
        private readonly IMapper _mapper;

        public GymInsuranceService(GymInsuranceRepository gymInsuranceRepo, IMapper mapper)
        {
            _gymInsuranceRepo = gymInsuranceRepo;
            _mapper = mapper;
        }

        // public async Task<IEnumerable<GymInsuranceReadDto>> GetAllGymInsurancesAsync()
        // {
        //     var gymInsurances = await _gymInsuranceRepo.GetAllAsync();
        //     return _mapper.Map<IEnumerable<GymInsuranceReadDto>>(gymInsurances);
        // }

        // public async Task<GymInsuranceReadDto> GetGymInsuranceByIdAsync(Guid id)
        // {
        //     var gymInsurance = await _gymInsuranceRepo.GetByIdAsync(id);
        //     return _mapper.Map<GymInsuranceReadDto>(gymInsurance);
        // }

        // public async Task AddGymInsuranceAsync(GymInsurance gymInsurance)
        // {
        //     await _gymInsuranceRepo.CreateOnAsync(gymInsurance);
        // }

        // public async Task UpdateGymInsuranceAsync(GymInsurance gymInsurance)
        // {
        //     await _gymInsuranceRepo.UpdateOnAsync(gymInsurance);
        // }

        // public async Task DeleteGymInsuranceAsync(GymInsurance gymInsurance)
        // {
        //     await _gymInsuranceRepo.DeleteOnAsync(gymInsurance);
        // }

        public async Task<GymInsuranceReadDto> CreateOnAsync(GymInsuranceCreateDto createDto)
        {
            var gymInsurance = _mapper.Map<GymInsuranceCreateDto, GymInsurance>(createDto);
            var gymInsuranceCreated = await _gymInsuranceRepo.CreateOnAsync(gymInsurance);
            return _mapper.Map<GymInsurance, GymInsuranceReadDto>(gymInsuranceCreated);
        }


        public async Task<List<GymInsuranceReadDto>> GetAllAsync()
        {
            var gymInsuranceList = await _gymInsuranceRepo.GetAllAsync();
            return _mapper.Map<List<GymInsurance>, List<GymInsuranceReadDto>>(gymInsuranceList);
        }

        public async Task<GymInsuranceReadDto> GetByIdAsync(Guid id)
        {
            var gymInsuranceFound = await _gymInsuranceRepo.GetByIdAsync(id);
            if (gymInsuranceFound == null)
            {
                return null;
            }
            return _mapper.Map<GymInsurance, GymInsuranceReadDto>(gymInsuranceFound);
        }

        public async Task<List<GymInsuranceReadDto>> GetByUserIdAsync(Guid userId)
{
    var gymInsurances = await _gymInsuranceRepo.GetByUserIdAsync(userId);
    return _mapper.Map<List<GymInsuranceReadDto>>(gymInsurances);
}
    

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var gymInsuranceFound = await _gymInsuranceRepo.GetByIdAsync(id);
            if (gymInsuranceFound == null)
            {
                return false;
            }

            return await _gymInsuranceRepo.DeleteOnAsync(gymInsuranceFound);
        }

        public async Task<bool> UpdateOnAsync(Guid id, GymInsuranceUpdateDto updateDto)
        {
            var gymInsuranceFound = await _gymInsuranceRepo.GetByIdAsync(id);
            if (gymInsuranceFound == null)
            {
                return false;
            }

            _mapper.Map(updateDto, gymInsuranceFound);
            return await _gymInsuranceRepo.UpdateOnAsync(gymInsuranceFound);
        }
    }
}
