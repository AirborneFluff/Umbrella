using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public sealed class AppRole : IdentityRole
{
    public override string Name { get; set; } = null!;

    public AppRole()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    public AppRole(string roleName) : this()
    {
        Id = Guid.NewGuid().ToString();
        Name = roleName;
    }
}