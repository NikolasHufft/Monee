using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ServerLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // General Department / Branch / Department
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }

        // Authentication // SystemRole / UserRole / RefreshTokenInfo
        public DbSet<User> Users { get; set; }

        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        // Country / City / Town                
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }

        // Employee / Overtime / OvertimeType / Vacation / Overtime / Sanction / SanctionType / Doctor / VacationType
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Overtime> OverTimes { get; set; }
        public DbSet<OvertimeType> OvertimeType { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Department>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Branch>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<GeneralDepartment>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<SystemRole>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Country>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<City>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Town>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Employee>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Overtime>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<OvertimeType>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Doctor>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Sanction>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<SanctionType>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Vacation>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<VacationType>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
        }
    }
}