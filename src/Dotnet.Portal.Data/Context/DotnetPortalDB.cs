using Dotnet.Portal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Portal.Data.Context
{
    public class DotnetPortalDB : DbContext
    {
        public DotnetPortalDB(DbContextOptions options) : base(options) { }

        public DbSet<Payment> Donations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Role> ListRoles { get; set; }
        public DbSet<MemberGroup> MemberGroups { get; set; }
        public DbSet<MemberRole> MemberRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DotnetPortalDB).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
