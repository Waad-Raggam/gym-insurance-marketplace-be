using System;
using System.ComponentModel.DataAnnotations;

namespace src.Entity
{
    public class GymInsurance
    {
        [Key]
        public Guid GIId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public bool IsActive { get; set; }

        // Foreign key for Gym
        public Guid GymId { get; set; }
        public Gym Gym { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
        public List<int> InsuranceIds { get; set; }
    }

}
