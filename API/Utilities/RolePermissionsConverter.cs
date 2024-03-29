﻿using API.Authentication;
using API.Entities;

namespace API.Utilities;

public static class RolePermissionsConverter
{
    public static IEnumerable<UserPermissions> ConvertToRoles(ulong permissions)
    {
        return Enum.GetValues<UserPermissions>()
            .Where(role => ((ulong)role & permissions) > 0)
            .AsEnumerable();
    }

    public static ulong ConvertToPermissionsValue(IEnumerable<AppRole> roles)
    {
        return roles
            .Select(role => (ulong)Enum.Parse<UserPermissions>(role.Name))
            .Aggregate((ulong)0, (permissions, roleFlag) => permissions | roleFlag);
    }
}