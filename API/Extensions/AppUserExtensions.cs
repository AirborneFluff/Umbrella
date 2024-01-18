using System.Collections.ObjectModel;
using System.Security.Claims;
using API.Authentication;
using API.Entities;
using API.Helpers;

namespace API.Extensions;

public static class AppUserExtensions
{
    public static IEnumerable<Claim> CreateClaims(this AppUser user, ulong permissions)
    {
        var permissionsHash = EnumUtilities.GetNameAndValueHash<UserPermissions>();
        
        return new Collection<Claim>()
        {
            new (ExtendedClaimTypes.Id, user.Id),
            new (ExtendedClaimTypes.Email, user.Email),
            new (ExtendedClaimTypes.OrganisationId, user.OrganisationId),
            new (ExtendedClaimTypes.Permissions, permissions.ToString()),
            new (ExtendedClaimTypes.PermissionsHash, permissionsHash)
        };
    }
}