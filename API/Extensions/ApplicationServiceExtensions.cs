using API.Data;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
    }
    
    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DataContext>(options => {
            var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
            if (connStr == null) throw new Exception("DefaultConnection not configured");
            
            options.UseSqlite(connStr);
        });
    }

    public static void AddIdentityCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<DataContext>();
        
        builder.Services.AddAuthentication();
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
    }
}