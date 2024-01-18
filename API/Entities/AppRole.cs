using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public sealed class AppRole : IdentityRole
{
    public ulong PermissionsFlag { get; set; }

    public AppRole()
    {
        Id = Guid.NewGuid().ToString();
    }

    public AppRole(string roleName) : this()
    {
        Name = roleName;
    }
    

    public AppRole(string roleName, ulong permissionsFlag) : this()
    {
        Name = roleName;
        PermissionsFlag = permissionsFlag;
    }
}