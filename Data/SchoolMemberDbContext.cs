using Microsoft.EntityFrameworkCore;
using webapi_playground.Models.Domain.Auth;
using webapi_playground.Models.Domain.Member;

namespace webapi_playground.Data;

public class SchoolMemberDbContext : DbContext
{

    public SchoolMemberDbContext(DbContextOptions<SchoolMemberDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserRolesDomain>()
            .HasOne(x => x.UserDomain) // From UserDomain class
            .WithMany(y => y.UserRolesDomain) // From both roles & user domain
            .HasForeignKey(z => z.UserID); // From UserRolesDomain

        modelBuilder.Entity<UserRolesDomain>()
            .HasOne(x => x.RoleDomain) // From RoleDomain class
            .WithMany(y => y.UserRolesDomain) // From both roles & user domain
            .HasForeignKey(z => z.RoleID); // From UserRolesDomain

    }

    public DbSet<MemberDomain> MemberTable { get; set; }
    public DbSet<UserDomain> UserTable { get; set; }
    public DbSet<RolesDomain> RolesTable { get; set; }
    public DbSet<UserRolesDomain> UserRolesTable { get; set; }

}
