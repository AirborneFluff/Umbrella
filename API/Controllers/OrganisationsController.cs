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
        var ownerId = Guid.NewGuid().ToString();
        
        var organisation = new Organisation()
        {
            Name = organisationDto.OrganisationName,
            OwnerId = ownerId
        };
        
        var owner = new AppUser
        {
            Id = ownerId,
            Email = organisationDto.Email,
            UserName = organisationDto.Email,
            Permissions = PermissionGroups.PowerUser,
            Organisation = organisation,
            OrganisationId = organisation.Id
        };
        
        organisation.Members.Add(owner);

        var result = await _userManager.CreateAsync(owner, organisationDto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        
        return BadRequest("There was an issue adding Organisation");
    }
}