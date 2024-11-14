using static src.DTO.GymDTO;


namespace src.Services.Gym
{
    public interface IGymService
    {
        // Task<GymInsuranceReadDto> CreateOneAsync(Guid userGuid, GymInsuranceCreateDto createDto);
        Task<GymReadDto> CreateOnAsync(Guid userGuid, GymCreateDto createDto);

        Task<List<GymReadDto>> GetAllAsync();

        Task<GymReadDto> GetByIdAsync(Guid id);
        Task<List<GymReadDto>> GetByUserIdAsync(Guid userId);
        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, GymUpdateDto updateDto);
    }
}
