using Dotnet.Portal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Portal.Data.Context
{
    public class DotnetPortalDB : DbContext
    {
        public DotnetPortalDB(DbContextOptions options) : base(options) { }

        public DbSet<Payment> Donations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Employee> Members { get; set; }
        public DbSet<Role> ListRoles { get; set; }
        public DbSet<EmployeeGroup> MemberGroups { get; set; }
        public DbSet<EmployeeRole> MemberRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DotnetPortalDB).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
