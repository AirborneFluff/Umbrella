using System.Security.Claims;
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
}