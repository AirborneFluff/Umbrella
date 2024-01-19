namespace API.Authentication;

[Flags]
public enum UserPermissions
{
    ManageUsers = 0x01,
    ManageStockSuppliers = 0x02,
    ManageStockItems = 0x04,
    ReadStockItems = 0x08,
    CreateStockItems = 0x10,
    EditStockItems = 0x20,
    DeleteStockItems = 0x40
}