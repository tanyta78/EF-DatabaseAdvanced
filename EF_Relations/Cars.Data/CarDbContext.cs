﻿namespace Cars.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CarDbContext:DbContext
    {
        public CarDbContext()
        {
            
        }

        public CarDbContext(DbContextOptions options):base(options)
        {
            
        }
        
        public DbSet<Car> Cars { get; set; }

        public DbSet<Make> Makes { get; set; }
        
        public DbSet<Engine> Engines { get; set; }

        public DbSet<LicensePlate> LicencePlates { get; set; }
        
        public DbSet<Dealership> Dealerships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(@"Server=DESKTOP-LAHCAG9\SQLEXPRESS;Database=Cars;Integrated Security=True;");
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //one to one
            builder.Entity<Car>()
                .HasOne(c => c.LicensePlate)
                .WithOne(lp => lp.Car)
                .HasForeignKey<LicensePlate>(lp => lp.CarId);
            
            
            builder.Entity<Engine>()
                .HasOne(e=>e.Cars)
                .WithMany(c=>c.)

            builder.Entity<CarDealership>()
                .HasKey(cd => new {cd.CarId, cd.DealershipId});

            //one to many opt.1
            //builder.Entity<CarDealership>()
            //    .HasOne(cd => cd.Car)
            //    .WithMany(c => c.CarDealerships)
            //    .HasForeignKey(cd => cd.CarId);

            //one to many opt.2
            builder.Entity<Car>()
                .HasMany(c => c.CarDealerships)
                .WithOne(cd => cd.Car)
                .HasForeignKey(cd => cd.CarId);

            builder.Entity<CarDealership>()
                .HasOne(cd => cd.Dealership)
                .WithMany(d => d.CarDealerships)
                .HasForeignKey(cd => cd.DealershipId);
        }
    }
}
