using System.Security.Claims;
using API.Authentication;
using API.Data.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public sealed class AccountController : BaseApiController
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public AccountController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IMapper mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == login.Email);
        if (user == null) return Unauthorized("Invalid login credentials");

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded) return Unauthorized("Invalid login credentials");
        
        var roleNames = await _userManager.GetRolesAsync(user);
        var roles = await _roleManager.GetRoles(roleNames);
        var userPermissions = RolePermissionsConverter.ConvertToPermissionsValue(roles);
        var claims = user.CreateClaims(userPermissions);

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity));

        var dto = _mapper.Map<AppUserDto>(user);
        dto.Permissions = userPermissions;

        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult> GetUser()
    {
        if (!User.Claims.Any()) return Ok();
        return Ok(User.GetDetails());
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        
        return Ok();
    }
}