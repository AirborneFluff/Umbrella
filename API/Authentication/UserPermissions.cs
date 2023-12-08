namespace API.Authentication;

public enum UserPermissions
{
    CreateAdministrators = 1 << 0,
    CreateUsers = 1 << 2,
    ManageUsers = 1 << 3,
    ManageStockItems = 1 << 4,
    ReadStockItems = 1 << 5
}