using AutoMapper;
using src.Repository;
using static src.DTO.GymDTO;

namespace src.Services.Gym
{
    public class GymService : IGymService
    {
        protected readonly GymRepository _gymRepo;
        protected readonly IMapper _mapper;

        public GymService(GymRepository gymRepo, IMapper mapper)
        {
            _gymRepo = gymRepo;
            _mapper = mapper;
        }

        public async Task<GymReadDto> CreateOnAsync( GymCreateDto createDto)
        {
            var gym = _mapper.Map<GymCreateDto, Entity.Gym>(createDto);
            // gym.OrderId = userGuid; // Uncomment if user ID is needed
            var gymCreated = await _gymRepo.CreateOnAsync(gym);
            return _mapper.Map<Entity.Gym, GymReadDto>(gymCreated);
        }

        public async Task<List<GymReadDto>> GetAllAsync()
        {
            var gymList = await _gymRepo.GetAllAsync();
            return _mapper.Map<List<Entity.Gym>, List<GymReadDto>>(gymList);
        }

        public async Task<GymReadDto> GetByIdAsync(Guid id)
        {
            var gymFound = await _gymRepo.GetByIdAsync(id);
            if (gymFound == null)
            {
                // Handle not found case (could throw an exception or return null)
                return null; // or throw new NotFoundException("Gym not found.");
            }
            return _mapper.Map<Entity.Gym, GymReadDto>(gymFound);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var gymFound = await _gymRepo.GetByIdAsync(id);
            if (gymFound == null)
            {
                return false; // Gym not found
            }

            return await _gymRepo.DeleteOnAsync(gymFound);
        }

        public async Task<bool> UpdateOneAsync(Guid id, GymUpdateDto updateDto)
        {
            var gymFound = await _gymRepo.GetByIdAsync(id);
            if (gymFound == null)
            {
                return false; // Gym not found
            }

            _mapper.Map(updateDto, gymFound);
            return await _gymRepo.UpdateOnAsync(gymFound);
        }

        // public Task<GymInsuranceReadDto> CreateOneAsync(GymInsuranceCreateDto createDto)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
