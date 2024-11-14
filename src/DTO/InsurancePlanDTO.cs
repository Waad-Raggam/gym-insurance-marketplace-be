using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.DTO
{
    public class InsurancePlanDTO
    {
       public class InsurancePlanCreateDto
{
    [Required]
            public string PlanName { get; set; }

            [Required]
            public decimal MonthlyPremium { get; set; }

            [Required]
            public string CoverageType { get; set; }

            [Required]
            public string PlanDescription { get; set; }

            public List<string> CoverageDetails { get; set; }
}
        public class InsurancePlanReadDto
        {
            public int insuranceId { get; set; }
            public string PlanName { get; set; }
            public decimal MonthlyPremium { get; private set; }
            public string CoverageType { get; private set; }
            public string PlanDescription { get; private set; }
            public List<string> CoverageDetails { get; private set; }
        }
        public class InsurancePlanUpdateDto
        {
            public int insuranceId { get; set; }
            public string PlanName { get; set; }
            public decimal MonthlyPremium { get; private set; }
            // public string CoverageType { get; private set; }
            public string planDescription { get; set; }
            // public List<string> CoverageDetails { get; private set; }
        }
    }
}
