using System.Security.Claims;
using API.Authentication;
using API.Data.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public sealed class AccountController : BaseApiController
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == login.Email);
        if (user == null) return NotFound();

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded) return Unauthorized();

        var permissionsHash = EnumUtilities.GetNameAndValueHash<UserPermissions>();

        var claims = new List<Claim>
        {
            new Claim(ExtendedClaimTypes.Id, user.Id),
            new Claim(ExtendedClaimTypes.Email, user.Email),
            new Claim(ExtendedClaimTypes.OrganisationId, user.OrganisationId),
            new Claim(ExtendedClaimTypes.Permissions, user.Permissions.ToString()),
            new Claim(ExtendedClaimTypes.PermissionsHash, permissionsHash)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity));

        return Ok(_mapper.Map<AppUser>(user));
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