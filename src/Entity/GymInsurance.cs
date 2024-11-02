using System;
using System.ComponentModel.DataAnnotations;

namespace src.Entity
{
    public class GymInsurance
    {
        [Key]
        public Guid GIId { get; set; }
        // public Gym Gym { get; set; } // Reference to the Order (Order property is originally nullable)
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public bool IsActive { get; set; }
         public Guid GymId { get; set; } // Foreign key for Order
    }
}
