using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace API.Authentication;

public sealed class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    private const string TicketIssuedTicks = nameof(TicketIssuedTicks);
    private readonly TimeSpan _absoluteLifespan = TimeSpan.FromDays(3);

    public override async Task SigningIn(CookieSigningInContext context)
    {
        context.Properties.SetString(
            TicketIssuedTicks,
            DateTimeOffset.UtcNow.Ticks.ToString());

        await base.SigningIn(context);
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var ticketIssuedTicksValue = context.Properties.GetString(TicketIssuedTicks);

        if (ticketIssuedTicksValue is null ||
            !long.TryParse(ticketIssuedTicksValue, out var ticketIssuedTicks))
        {
            await RejectPrincipalAsync(context);
            return;
        }

        var ticketIssuedUtc = new DateTimeOffset(ticketIssuedTicks, TimeSpan.FromHours(0));

        if (DateTimeOffset.UtcNow - ticketIssuedUtc > _absoluteLifespan)
        {
            await RejectPrincipalAsync(context);
            return;
        }

        await base.ValidatePrincipal(context);
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        if (context.Response.HasStarted) return Task.CompletedTask;
        
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    }

    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        if (context.Response.HasStarted) return Task.CompletedTask;
        
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    }

    private static async Task RejectPrincipalAsync(CookieValidatePrincipalContext context)
    {
        if (context.Response.HasStarted) return;
        
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync();
    }
}