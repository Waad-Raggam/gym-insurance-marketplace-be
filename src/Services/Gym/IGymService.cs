using static src.DTO.GymDTO;


namespace src.Services.Gym
{
    public interface IGymService
    {
        // Task<GymInsuranceReadDto> CreateOneAsync(Guid userGuid, GymInsuranceCreateDto createDto);
        Task<GymReadDto> CreateOnAsync(GymCreateDto createDto);

        Task<List<GymReadDto>> GetAllAsync();

        Task<GymReadDto> GetByIdAsync(Guid id);

        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, GymUpdateDto updateDto);
    }
}
