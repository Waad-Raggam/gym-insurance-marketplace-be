using static src.DTO.GymDTO;

namespace src.DTO
{
    public class GymInsuranceCreateDto
    {
        public Guid GymId { get; set; }
        // public string GymName { get; set; }
        // public int InsurancePlanId { get; set; }
        // public string InsurancePlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public bool IsActive { get; set; }
    }
    public class GymInsuranceReadDto
    {
        public Guid GIId { get; set; }
        public Guid GymId { get; set; }
        public GymReadDto Gym { get; set; } // TODO: Include Gym information
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public bool IsActive { get; set; }
    }
    public class GymInsuranceUpdateDto
    {
        // public int GymInsuranceId { get; set; }
        public Guid GymId { get; set; }
        // public string GymName { get; set; }
        // public int InsurancePlanId { get; set; }
        // public string InsurancePlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
