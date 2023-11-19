using System.Security.Claims;
using API.Data.DTOs;
using API.Data.Records;
using API.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == login.Email);
        if (user == null) return NotFound();

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded) return Unauthorized();
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email!),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);

        //TODO We need some response from the server. IdentityUserDto perhaps
        //TODO Convert the User.GetDetails from principle extension to something that can be used here
        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetUser()
    {
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