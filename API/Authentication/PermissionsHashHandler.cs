using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace API.Authentication;

public sealed class PermissionsHashHandler : AuthorizationHandler<PermissionsHashRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionsHashHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsHashRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        if (!context.User.HasClaim(c => c.Type == requirement.ClaimType && c.Value == requirement.ExpectedValue))
        {
            await httpContext!.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            httpContext!.Response.StatusCode = 401;
            await httpContext.Response.CompleteAsync();
            return;
        }
        
        context.Succeed(requirement);
    }
}