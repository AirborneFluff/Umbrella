using API.ActionFilters;
using API.Authentication;
using API.Data;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
    }

    public static void AddCosmosDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DataContext>(options =>
        {
            var uri = builder.Configuration["Cosmos:URI"];
            var key = builder.Configuration["Cosmos:PrimaryKey"];
            var name = builder.Configuration["Cosmos:DatabaseName"];
            if (uri is null || key is null || name is null) throw new Exception("Cosmos DB not configured");

            options.UseCosmos(uri, key, name);
        });
    }

    public static void AddIdentityCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AuthenticationContext>(options => {
            var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
            if (connStr == null) throw new Exception("DefaultConnection not configured");

            options.UseSqlite(connStr);
        });
        
        builder.Services
            .AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AuthenticationContext>();
    }

    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(600);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;
                
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
            });
        
        builder.Services.AddTransient<CustomCookieAuthenticationEvents>();
    }

    public static void AddAuthorization(this WebApplicationBuilder builder)
    {
        var permissionsHash = EnumUtilities.GetNameAndValueHash<UserPermissions>();
        
        builder.Services.AddHttpContextAccessor();
        var defaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(
                new PermissionsHashRequirement(permissionsHash))
            .Build();
        
        
        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = defaultPolicy;
            foreach (var permission in Enum.GetValues<UserPermissions>())
            {
                options.AddPolicy(permission.ToString(),
                    policy =>
                    {
                        policy.AddRequirements(new PermissionsHashRequirement(permissionsHash));
                        policy.RequireAssertion(ctx =>
                            ctx.User.HasPermission(permission));
                    });
            }
        });
        
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionsHashHandler>();
    }
    
    public static void AddActionFilters(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ValidateStockItemExists>();
    }
}