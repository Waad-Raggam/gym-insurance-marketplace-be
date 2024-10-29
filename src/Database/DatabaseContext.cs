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
        }
    } // end class
} // end namespace
