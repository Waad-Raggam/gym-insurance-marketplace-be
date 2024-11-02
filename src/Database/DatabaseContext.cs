using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using src.Entity;

namespace src.Database
{
    public class DatabaseContext : DbContext // DatabaseContext inherits from DbContext
    {
        /// <summary>
        /// The point of the database is to hold classes or configurations related to database setup
        /// </summary>

        public DbSet<Users> User { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderGemstone> OrderGemstones { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Cart> Cart { get; set; }

        // public DbSet<Gemstones_Carvings> Gemstones_Carvings { get; set; }
        public DbSet<Gemstones> Gemstones { get; set; }
        public DbSet<Jewelry> Jewelry { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Gym> Gym { get; set; }
        public DbSet<InsurancePlan> InsurancePlan { get; set; }
        public DbSet<GymInsurance> GymInsurance { get; set; }

        // public DbSet<PaymentCard> PaymentCard { get; set; }

        // public DbSet<OrderGemstone> OrderGemstone { get; set; }

        // Constructor
        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);
            modelBuilder.HasPostgresEnum<Role>();

            modelBuilder.Entity<InsurancePlan>().HasData(
        new InsurancePlan(1, "Basic Plan", 30m, "Basic Coverage", "Accidental injuries, Limited access to gym facilities, 24/7 customer support"),
        new InsurancePlan(2, "Standard Plan", 50m, "Comprehensive Coverage", "Accidental injuries, Gym facility access, Personal trainer sessions, Nutrition consultations, 24/7 customer support"),
        new InsurancePlan(3, "Premium Plan", 80m, "Premium Comprehensive", "Accidental injuries, Gym facility access, Unlimited personal trainer sessions, Nutrition consultations, Mental health support, Specialized fitness programs, 24/7 customer support")
    );

     modelBuilder.Entity<GymInsurance>()
        .HasOne(gi => gi.Gym)
        .WithMany() // TODO: specify a collection in Gym
        .HasForeignKey(gi => gi.GymId)
        .OnDelete(DeleteBehavior.Cascade);

            // Define the Gym to GymInsurance relationship
            // modelBuilder.Entity<GymInsurance>()
            //     .HasOne(gi => gi.Gym)
            //     .WithMany() // or .WithMany(g => g.GymInsurances) if Gym has a collection of GymInsurances
            //     .HasForeignKey(gi => gi.GymId)
            //     .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<GymInsurance>()
            //     .HasOne(gi => gi.InsurancePlan)
            //     .WithMany() // or .WithMany(g => g.GymInsurances) if Gym has a collection of GymInsurances
            //     .HasForeignKey(gi => gi.InsurancePlanId)
            //     .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<InsurancePlan>().HasData(
            //    new InsurancePlan
            //    {
            //        Id = 1,
            //        PlanName = "Basic Plan",
            //        MonthlyPremium = 30.00m,
            //        CoverageType = "Basic Coverage",
            //        CoverageDetails = "Accidental injuries, Limited access to gym facilities, 24/7 customer support"
            //    },
            //    new InsurancePlan
            //    {
            //        Id = 2,
            //        PlanName = "Standard Plan",
            //        MonthlyPremium = 50.00m,
            //        CoverageType = "Comprehensive Coverage",
            //        CoverageDetails = "Accidental injuries, Gym facility access, Personal trainer sessions, Nutrition consultations, 24/7 customer support"
            //    },
            //    new InsurancePlan
            //    {
            //        Id = 3,
            //        PlanName = "Premium Plan",
            //        MonthlyPremium = 80.00m,
            //        CoverageType = "Premium Comprehensive",
            //        CoverageDetails = "Accidental injuries, Gym facility access, Unlimited personal trainer sessions, Nutrition consultations, Mental health support, Specialized fitness programs, 24/7 customer support"
            //    });
        }
    } // end class
} // end namespace
