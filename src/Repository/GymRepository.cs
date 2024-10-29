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

        public async Task<Gym> CreateOnAsync(Gym newGym)
        {
            await _gym.AddAsync(newGym);
            await _databaseContext.SaveChangesAsync();
            return newGym;
        }
    }
}