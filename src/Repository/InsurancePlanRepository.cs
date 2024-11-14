using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class InsurancePlanRepository
    {
        protected DbSet<InsurancePlan> _insurancePlan;
        protected DatabaseContext _databaseContext;

        public InsurancePlanRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _insurancePlan = databaseContext.Set<InsurancePlan>();
        }

        public async Task<List<InsurancePlan>> GetAllAsync()
        {
            return await _insurancePlan.ToListAsync();
        }

        public async Task<InsurancePlan?> GetByIdAsync(int id)
        {
            return await _insurancePlan.FindAsync(id);
        }

        public async Task<List<InsurancePlan>> SearchInsurancePlansByCoverageType(string coverageType)
        {
            var query = _insurancePlan.AsQueryable();

            if (!string.IsNullOrEmpty(coverageType))
            {
                query = query.Where(plan => plan.CoverageDetails.Contains(coverageType));
            }

            return await query.ToListAsync();
        }
        public async Task CreateAsync(InsurancePlan plan)
{
    await _insurancePlan.AddAsync(plan);
    await _databaseContext.SaveChangesAsync();
}


public async Task UpdateAsync(InsurancePlan plan)
{
    _insurancePlan.Update(plan);  
    await _databaseContext.SaveChangesAsync();
}

        public async Task DeleteAsync(InsurancePlan plan)
        {
            _insurancePlan.Remove(plan);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
