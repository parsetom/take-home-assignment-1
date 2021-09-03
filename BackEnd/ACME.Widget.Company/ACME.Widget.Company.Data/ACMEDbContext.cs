using ACME.Widget.Company.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Data
{
    public class ACMEDbContext : DbContext
    {
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityRegistration> ActivityRegistrations { get; set; }

        public ACMEDbContext()
        {
        }

        private bool isEFTools = true;
        public ACMEDbContext(bool isEFTools = true)
        {
            this.isEFTools = isEFTools;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (isEFTools)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Database=ACMEWidget;Integrated Security=sspi;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var personEntity = modelBuilder.Entity<Person>();
            personEntity.HasKey(p => p.Id);
            personEntity.Property(p => p.Id).ValueGeneratedOnAdd();
            personEntity.HasIndex(p => p.Email).IsUnique();
            personEntity.Property(p => p.Email).IsRequired().HasMaxLength(50);
            personEntity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            personEntity.Property(p => p.LastName).IsRequired().HasMaxLength(50);

            var activityEntity = modelBuilder.Entity<Activity>();
            activityEntity.HasKey(a => a.Id);
            activityEntity.Property(p => p.Id).ValueGeneratedOnAdd();
            activityEntity.HasIndex("Name", "StartDate").IsUnique();
            activityEntity.Property(a => a.Name).IsRequired().HasMaxLength(100);

            var activityRegistrationEntity = modelBuilder.Entity<ActivityRegistration>();
            activityRegistrationEntity.HasKey(r => r.Id);
            activityRegistrationEntity.Property(r => r.Id).ValueGeneratedOnAdd();
            activityRegistrationEntity.HasIndex("PersonId", "ActivityId").IsUnique();
            activityRegistrationEntity.Property(r => r.Comments).HasMaxLength(255);

            modelBuilder.Entity<Activity>().HasData(
                new Activity
                {
                    Id = 1,
                    Name = "Oktoberfest (VIP)",
                    RegistrationDeadline = new DateTime(2021, 09, 20),
                    StartDate = new DateTime(2021, 09, 24),
                    EndDate = new DateTime(2021, 10, 11)
                },
                new Activity
                {
                    Id = 2,
                    Name = "Wine Tasting Conference",
                    RegistrationDeadline = new DateTime(2021, 09, 01),
                    StartDate = new DateTime(2021, 09, 15, 10, 0, 0),
                    EndDate = new DateTime(2021, 09, 15, 14, 0, 0)
                },
                new Activity
                {
                    Id = 3,
                    Name = "Weekly Jogging at Campus Garden",
                    RegistrationDeadline = DateTime.MaxValue,
                    EndDate = DateTime.MaxValue
                });

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Joseph",
                    LastName = "Dela Cruz",
                    Email = "test@yopmail.com"
                });

            modelBuilder.Entity<ActivityRegistration>().HasData(
                new ActivityRegistration
                {
                    Id = 1,
                    ActivityId = 3,
                    PersonId = 1,
                    Comments = "Yay!"
                });
        }
    }
}
