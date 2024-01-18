using System.Security.Claims;
using API.Authentication;
using API.Data.DTOs;
using API.Data.Records;

namespace API.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static AppUserDto GetDetails(this ClaimsPrincipal principal)
    {
        var userClaims = principal.Claims.ToDictionary(x => x.Type, x => x.Value);

        var id = userClaims.GetValueOrThrow(ExtendedClaimTypes.Id, "User had no Id");
        var email = userClaims.GetValueOrThrow(ExtendedClaimTypes.Email, "User had no Email");
        var organisationId = userClaims.GetValueOrThrow(ExtendedClaimTypes.OrganisationId, "User had no OrganisationId");
        var permissions  = userClaims.GetValueOrThrow(ExtendedClaimTypes.Permissions, "User had no Permissions Value");

        return new AppUserDto()
        {
            Id = id,
            Email = email,
            OrganisationId = organisationId,
            Permissions = permissions
        };
    }

    public static string GetOrganisationId(this ClaimsPrincipal principal)
    {
        var userClaims = principal.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();
        var organisationId = userClaims.Find(claim => claim.Type == ExtendedClaimTypes.OrganisationId);
        if (organisationId == null) throw new Exception("User has no organisation Id");

        return organisationId.Value;
    }

    public static bool HasPermission(this ClaimsPrincipal principal, UserPermissions permission)
    {
        var permissionsClaims = principal.Claims
            .FirstOrDefault(x => x.Type == ExtendedClaimTypes.Permissions);
        if (permissionsClaims is null) return false;

        var bitwise = ulong.Parse(permissionsClaims.Value) & (ulong)permission;
        return bitwise > 0;
    }
}