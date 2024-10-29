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

    }
}