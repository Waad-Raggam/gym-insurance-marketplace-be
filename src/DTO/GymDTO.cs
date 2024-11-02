using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.DTO
{
    public class GymDTO
    {
        public class GymCreateDto
        {
            [Required]
            public string GymName { get; set; }

            [Required]
            public string Location { get; set; }

            public string ContactName { get; set; }

            [Phone]
            public string ContactPhone { get; set; }

            [EmailAddress]
            public string ContactEmail { get; set; }

            [Range(0, double.MaxValue)]
            public double AnnualRevenue { get; set; }

            [Range(0, int.MaxValue)]
            public int MemberCapacity { get; set; }

            [Range(0, int.MaxValue)]
            public int NumberOfEmployees { get; set; }

            public string HoursOfOperation { get; set; }

            public List<string> Facilities { get; set; } = new List<string>();
            public List<string> Services { get; set; } = new List<string>();
        }

        public class GymReadDto
        {
            // public Guid InsuranceId { get; set; }
            public Guid GymId { get; set; }
            public string GymName { get; set; }
            public string Location { get; set; }
            public string ContactName { get; set; }
            public string ContactPhone { get; set; }
            public string ContactEmail { get; set; }
            public double AnnualRevenue { get; set; }
            public int MemberCapacity { get; set; }
            public int NumberOfEmployees { get; set; }
            public string HoursOfOperation { get; set; }
            public List<string> Facilities { get; set; }
            public List<string> Services { get; set; }
        }

        public class GymUpdateDto
        {
            // public Guid InsuranceId { get; set; }
            public Guid GymId { get; set; }
            public string GymName { get; set; }
        }
    }
}
