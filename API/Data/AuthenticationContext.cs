using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class AuthenticationContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DbSet<Organisation> Organisations => Set<Organisation>();
    
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>()
            .HasOne(user => user.Organisation)
            .WithMany(org => org.Members);

        modelBuilder.Entity<Organisation>()
            .HasIndex(o => o.NormalizedName)
            .IsUnique();
    }
}