using Microsoft.EntityFrameworkCore;
using IntraApi.Domain.Entities;
using IntraApi.Domain.Common;
using IntraApi.Application.Contracts;
using System.Numerics;

namespace IntraApi.Persistence
{
    public class IntraApiDBContext : DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;
        public IntraApiDBContext(DbContextOptions<IntraApiDBContext> options)
            : base(options)
        {
        }

        public IntraApiDBContext(DbContextOptions<IntraApiDBContext> options, ILoggedInUserService loggedInUserService)
            : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWorkQuota> EmployeeWorkQuotas { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<EmployeeShift> EmployeeShifts { get; set; }
        public DbSet<EmployeeShiftView> EmployeeShiftsV { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IntraApiDBContext).Assembly);

            modelBuilder.Entity<User>().ToTable("Users", "umUser");
            modelBuilder.Entity<Employee>().ToTable("Employees", "staffing");
            modelBuilder.Entity<EmployeeWorkQuota>().ToTable("EmployeeWorkQuota", "staffing");
            modelBuilder.Entity<Role>().ToTable("Roles", "staffing");
            modelBuilder.Entity<EmployeeRole>().ToTable("EmployeeRoles", "staffing");
            modelBuilder.Entity<EmployeeShift>().ToTable("EmployeeShifts", "staffing");
            modelBuilder.Entity<EmployeeShiftView>().HasNoKey().ToView("EmployeeShiftsV", "staffing");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                entry.Entity.DModify = DateTime.Now;
                entry.Entity.UserID = _loggedInUserService?.UserId ?? throw new InvalidOperationException("No logged-in user found.");
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
