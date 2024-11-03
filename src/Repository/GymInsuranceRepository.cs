using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class GymInsuranceRepository
    {
        protected DbSet<GymInsurance> _gymInsurance;
        protected DatabaseContext _databaseContext;

        public GymInsuranceRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _gymInsurance = databaseContext.Set<GymInsurance>();
        }

        public async Task<GymInsurance> CreateOnAsync(GymInsurance newGymInsurance)
        {
            await _gymInsurance.AddAsync(newGymInsurance);
            await _databaseContext.SaveChangesAsync();
            return newGymInsurance;
        }

        public async Task<List<GymInsurance>> GetAllAsync()
        {
            return await _gymInsurance.ToListAsync();
        }

        public async Task<GymInsurance?> GetByIdAsync(Guid id)
        {
            return await _gymInsurance.FindAsync(id);
        }

        public async Task<bool> DeleteOnAsync(GymInsurance gymInsurance)
        {
            _gymInsurance.Remove(gymInsurance);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOnAsync(GymInsurance gymInsurance)
        {
            _gymInsurance.Update(gymInsurance);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}