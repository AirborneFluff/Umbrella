using System.Security.Claims;
using API.Authentication;
using API.Data.DTOs;
using API.Data.Records;

namespace API.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static IdentityUserDto GetDetails(this ClaimsPrincipal principal)
    {
        var userClaims = principal.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();
        var email = userClaims.Find(claim => claim.Type == ClaimTypes.Name);
        if (email == null) throw new Exception("User has no email");
        
        return new IdentityUserDto()
        {
            Claims = userClaims,
            Email = email.Value
        };
    }

    public static IdentityRoles.Role MaxPermissibleRole(this ClaimsPrincipal principal)
    {
        var userRoles = principal.Claims
            .Where(x => x.Type == ClaimTypes.Role)
            .Select(x => x.Value);
        var maxRole = (IdentityRoles.Role?) userRoles.Max(role => Enum.Parse(typeof(IdentityRoles.Role), role));
        if (maxRole is null) throw new Exception("User has no roles");
        var permissibleValue = (int)maxRole + 1;
        
        return (IdentityRoles.Role)permissibleValue;
    }
}