using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}