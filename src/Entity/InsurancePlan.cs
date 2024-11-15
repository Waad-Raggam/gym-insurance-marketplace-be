using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.Entity
{
    public class InsurancePlan
    {
        [Key]
        public int InsuranceId { get; set; }
        public string PlanName { get; set; }
        public decimal MonthlyPremium { get; set; }
        public string CoverageType { get; set; }
        public string PlanDescription{ get; set; }
        public List<string> CoverageDetails { get; set; }

        public InsurancePlan(int id, string planName, decimal monthlyPremium, string coverageType, string planDescription , List<string> coverageDetails)
        {
            InsuranceId = id;
            PlanName = planName;
            MonthlyPremium = monthlyPremium;
            CoverageType = coverageType;
            PlanDescription = planDescription;
            CoverageDetails = coverageDetails;
        }

        private InsurancePlan() { }
    }
}
