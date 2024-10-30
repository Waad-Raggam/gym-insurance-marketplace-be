using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Entity
{
    public class InsurancePlan
    {
        public int Id { get; private set; }
        public string PlanName { get; private set; }
        public decimal MonthlyPremium { get; private set; }
        public string CoverageType { get; private set; }
        public string CoverageDetails { get; private set; }

        public InsurancePlan(int id, string planName, decimal monthlyPremium, string coverageType, string coverageDetails)
        {
            Id = id;
            PlanName = planName;
            MonthlyPremium = monthlyPremium;
            CoverageType = coverageType;
            CoverageDetails = coverageDetails;
        }
    }

}