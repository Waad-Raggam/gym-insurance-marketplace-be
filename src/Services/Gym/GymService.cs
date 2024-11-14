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

        public async Task<GymReadDto> CreateOnAsync(Guid userIdPassed, GymCreateDto createDto)
        {
            var gym = _mapper.Map<GymCreateDto, Entity.Gym>(createDto);
            Guid userId = gym.UserId;
            Console.WriteLine("Passed " + userIdPassed);
            Console.WriteLine("Assigned " + userId);
            // gym.UserId = userGuid; 
            var gymCreated = await _gymRepo.CreateOnAsync(userId, gym);
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
        public async Task<List<GymReadDto>> GetByUserIdAsync(Guid userId)
        {
            var gymsFound = await _gymRepo.GetByUserIdAsync(userId);
            if (gymsFound == null || gymsFound.Count == 0)
            {
                // Handle not found case
                return null; // or throw new NotFoundException("No gyms found for the specified user.");
            }
            return _mapper.Map<List<Entity.Gym>, List<GymReadDto>>(gymsFound);
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
