using API.Authentication;
using API.Data;
using API.Data.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public sealed class OrganisationsController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AuthenticationContext _context;

    public OrganisationsController(UserManager<AppUser> userManager, AuthenticationContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> CreateNewOrganisation([FromBody] NewOrganisationDto organisationDto)
    {
        var organisationId = Guid.NewGuid().ToString();
        var owner = new AppUser
        {
            Email = organisationDto.Email,
            UserName = organisationDto.Email,
            OrganisationId = organisationId,
            Permissions = PermissionGroups.PowerUser
        };

        var result = await _userManager.CreateAsync(owner, organisationDto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        var organisation = new Organisation
        {
            OwnerId = owner.Id,
            Name = organisationDto.OrganisationName
        };

        _context.Organisations.Add(organisation);
        try
        {
            if (await _context.SaveChangesAsync() > 0) return Ok();
        }
        catch (DbUpdateException exception)
        {
            await _userManager.DeleteAsync(owner);
            return BadRequest(exception.InnerException?.Message);
        }
        
        return BadRequest("There was an issue adding Organisation");
    }
}