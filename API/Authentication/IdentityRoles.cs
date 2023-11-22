namespace API.Authentication;

public static class IdentityRoles
{
    public const string Owner = nameof(Role.Owner);
    public const string Administrator = nameof(Role.Administrator);
    public const string User = nameof(Role.User);
    public const string ReadOnlyUser =nameof(Role.ReadOnlyUser);

    public enum Role
    {
        Owner,
        Administrator,
        User,
        ReadOnlyUser
    }

    public static int? GetRoleValue(string role)
    {
        return (int?)Enum.Parse(typeof(Role), role, true);
    }
    public static Role? GetRole(string role)
    {
        return (Role?)Enum.Parse(typeof(Role), role, true);
    }
}