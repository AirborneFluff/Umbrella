using API.ActionResults;
using API.Authentication;
using API.Data.DTOs;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "RequireAdministratorRole")]
public sealed class AdminController : BaseApiController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AdminController> _logger;
    private readonly IMapper _mapper;

    public AdminController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AdminController> logger,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("users")]
    public async Task<ActionResult> AddNewUser([FromBody] NewUserDto userData)
    {
        var user = new IdentityUser()
        {
            UserName = userData.Email,
            Email = userData.Email
        };
        
        var result = await _userManager.CreateAsync(user, userData.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        foreach (var role in userData.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role)) continue;
            if (User.MaxPermissibleRole() > IdentityRoles.GetRole(role)) continue;
            
            await _userManager.AddToRoleAsync(user, role);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new UserUpdateDto()
        {
            Id = user.Id,
            Email = user.Email,
            Roles = roles
        });
    }

    [HttpPost("users/{userId}/roles/{role}")]
    public async Task<ActionResult> AddUserRole(string role, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound("No user found by that Id");

        if (!await _roleManager.RoleExistsAsync(role)) return NotFound("No role found");

        if (User.MaxPermissibleRole() > IdentityRoles.GetRole(role))
            return new ForbiddenObjectResult("You do not have permission to add this role");
        
        await _userManager.AddToRoleAsync(user, role);
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new UserUpdateDto()
        {
            Id = user.Id,
            Email = user.Email!,
            Roles = roles
        });
    }

    [HttpDelete("users/{userId}/roles/{role}")]
    public async Task<ActionResult> RemoveUserRole(string role, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound("No user found by that Id");

        if (!await _roleManager.RoleExistsAsync(role)) return NotFound("No role found");

        if (User.MaxPermissibleRole() > IdentityRoles.GetRole(role))
            return new ForbiddenObjectResult("You do not have permission to remove this role");
        
        await _userManager.RemoveFromRoleAsync(user, role);
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new UserUpdateDto()
        {
            Id = user.Id,
            Email = user.Email!,
            Roles = roles
        });
    }
}