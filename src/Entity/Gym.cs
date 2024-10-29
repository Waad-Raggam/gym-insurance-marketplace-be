using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Entity
{
    public class Gym
    {
        [Key]
        public Guid GymId { get; set; }

        [Required]
        public string BusinessName { get; set; }

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
}
