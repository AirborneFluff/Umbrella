namespace API.Authentication;

[Flags]
public enum UserPermissions
{
    ManageUsers = 0x01,
    ManageStockSuppliers = 0x02,
    ManageStockItems = 0x04,
    ReadStockItems = 0x08
}