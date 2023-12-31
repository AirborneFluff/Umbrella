﻿using System.Security.Claims;
using API.Authentication;
using API.Data.DTOs;
using API.Data.Records;

namespace API.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static AppUserDto GetDetails(this ClaimsPrincipal principal)
    {
        var userClaims = principal.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();
        
        var id = userClaims.Find(claim => claim.Type == ExtendedClaimTypes.Id);
        var email = userClaims.Find(claim => claim.Type == ExtendedClaimTypes.Email);
        var permissions = userClaims.Find(claim => claim.Type == ExtendedClaimTypes.Permissions);
        var hash = userClaims.Find(claim => claim.Type == ExtendedClaimTypes.PermissionsHash);
        
        if (id == null) throw new Exception("User has no id");
        if (email == null) throw new Exception("User has no email");
        if (permissions == null) throw new Exception("User has no permissions");
        if (hash == null) throw new Exception("User has no hash");
        
        return new AppUserDto()
        {
            Id = id.Value,
            Email = email.Value,
            Permissions = ulong.Parse(permissions.Value),
            PermissionsHash = hash.Value
        };
    }

    public static bool HasPermission(this ClaimsPrincipal principal, UserPermissions permission)
    {
        var permissionsClaims = principal.Claims
            .FirstOrDefault(x => x.Type == ExtendedClaimTypes.Permissions);
        if (permissionsClaims is null) return false;

        var permissionBit = (ulong)1 << (int)permission;
        var bitwise = ulong.Parse(permissionsClaims.Value) & permissionBit;
        return bitwise > 0;
    }
}