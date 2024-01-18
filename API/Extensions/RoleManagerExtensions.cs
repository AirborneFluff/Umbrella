using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions;

public static class RoleManagerExtensions
{
    public static async Task<IEnumerable<AppRole>> GetRoles(this RoleManager<AppRole> roleManager, IEnumerable<string> roleNames)
    {
        var tasks = roleNames
            .Select(roleManager.FindByNameAsync);
        var roleResult = await Task.WhenAll(tasks);
        return roleResult.OfType<AppRole>();
    }
}