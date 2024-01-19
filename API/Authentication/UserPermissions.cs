namespace API.Authentication;

[Flags]
public enum UserPermissions
{
    ManageUsers = 0x01,
    ReadStockItems = 0x02,
    CreateStockItems = 0x04,
    EditStockItems = 0x08,
    DeleteStockItems = 0x10
}