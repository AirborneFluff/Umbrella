namespace API.Authentication;

public static class PermissionGroups
{
    public static ulong PowerUser => GetPermissionValue(_powerUser);
    public static ulong StoresManager => GetPermissionValue(_storesManager);
    
    private static UserPermissions[] _powerUser => (UserPermissions[])Enum.GetValues(typeof(UserPermissions));
    private static readonly UserPermissions[] _storesManager =
    {
        
        UserPermissions.ManageStockItems,
        UserPermissions.ManageStockSuppliers,
        UserPermissions.ReadStockItems
    };


    static ulong GetPermissionValue(UserPermissions[] permissions)
    {
        return permissions
            .Aggregate<UserPermissions, ulong>(0, (current, value) => current | (ulong)1 << (int)value);
    }
}