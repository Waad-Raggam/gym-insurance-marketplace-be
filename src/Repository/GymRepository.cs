using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class GymRepository
    {
        protected DbSet<Gym> _gym;
        protected DatabaseContext _databaseContext;

        public GymRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _gym = databaseContext.Set<Gym>();
        }

        public async Task<Gym> CreateOnAsync(Guid userId, Gym newGym)
        {
            await _gym.AddAsync(newGym);
            await _databaseContext.SaveChangesAsync();
            return newGym;
        }

        public async Task<List<Gym>> GetAllAsync()
        {
            return await _gym.ToListAsync();
        }

        public async Task<Gym?> GetByIdAsync(Guid id)
        {
            return await _gym.FindAsync(id);
        }
        public async Task<List<Gym>> GetByUserIdAsync(Guid userId)
        {
            return await _gym.Where(g => g.UserId == userId).ToListAsync();
        }
        public async Task<bool> DeleteOnAsync(Gym gym)
        {
            _gym.Remove(gym);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOnAsync(Gym updateGym)
        {
            _gym.Update(updateGym);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}