using Microsoft.AspNetCore.Authorization;

namespace API.Authentication;

public sealed class PermissionsHashRequirement : IAuthorizationRequirement
{
    public string ClaimType { get; } = ExtendedClaimTypes.PermissionsHash;
    public string ExpectedValue { get; }

    public PermissionsHashRequirement(string expectedValue)
    {
        ExpectedValue = expectedValue ?? throw new ArgumentNullException(nameof(expectedValue));
    }
}