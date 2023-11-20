﻿using API.Authentication;
using API.Data;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        builder.Services
            .AddDefaultIdentity<IdentityUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddRoleValidator<RoleValidator<IdentityRole>>()
            .AddEntityFrameworkStores<DataContext>();
    }

    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            { 
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;
                
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
            });
        
        builder.Services.AddTransient<CustomCookieAuthenticationEvents>();
    }

    public static void AddAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireOwnerRole",
                policy => policy.RequireRole(IdentityRoleNames.Owner));
            
            options.AddPolicy("RequireAdministratorRole",
                policy => policy.RequireRole(
                    IdentityRoleNames.Administrator,
                    IdentityRoleNames.Owner));
            
            options.AddPolicy("RequireUserRole",
                policy => policy.RequireRole(
                    IdentityRoleNames.User,
                    IdentityRoleNames.Administrator,
                    IdentityRoleNames.Owner));
        });
    }
}