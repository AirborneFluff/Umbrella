using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class AuthenticationContext : IdentityDbContext<AppUser>
{
    public DbSet<Organisation> Organisations => Set<Organisation>();
    
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>()
            .HasOne(user => user.Organisation);

        modelBuilder.Entity<Organisation>()
            .HasMany(o => o.Members)
            .WithOne(au => au.Organisation);
        
        modelBuilder.Entity<Organisation>()
            .HasOne(o => o.Owner)
            .WithOne(au => au.Organisation)
            .HasForeignKey<Organisation>(o => o.OwnerId);

        modelBuilder.Entity<Organisation>()
            .HasIndex(o => o.NormalizedName)
            .IsUnique();

        modelBuilder.Entity<OrganisationMember>()
            .HasKey(member => new { member.MemberId, member.OrganisationId });

    }
}