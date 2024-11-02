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
        public Guid GymId { get; set; } // Foreign key
        public Gym Gym { get; set; } // Navigation property to Gym

        // Foreign key for Plan
        public int InsuranceId { get; set; } // Foreign key
        public InsurancePlan InsurancePlan { get; set; } // Navigation property to Gym
    }
}
