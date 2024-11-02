using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.DTO
{
    public class InsurancePlanDTO
    {
        public class InsurancePlanReadDto
        {
            public int insuranceId { get; set; }
            public string PlanName { get; set; }
            public decimal MonthlyPremium { get; private set; }
            public string CoverageType { get; private set; }
            public string CoverageDetails { get; private set; }
        }
    }
}
