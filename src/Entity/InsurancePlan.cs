using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Entity
{
    public class InsurancePlan
    {
        [Key]
        public int InsuranceId { get; set; }
        public string PlanName { get; set; }
        public decimal MonthlyPremium { get; set; }
        public string CoverageType { get; set; }
        public string CoverageDetails { get; set; }

        public InsurancePlan(int id, string planName, decimal monthlyPremium, string coverageType, string coverageDetails)
        {
            InsuranceId = id;
            PlanName = planName;
            MonthlyPremium = monthlyPremium;
            CoverageType = coverageType;
            CoverageDetails = coverageDetails;
        }
        private InsurancePlan() { }
    }

}