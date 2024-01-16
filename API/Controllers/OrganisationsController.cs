using API.Authentication;
using API.Data.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class OrganisationsController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public OrganisationsController(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
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
        
        return Ok(_mapper.Map<AppUserDto>(owner));
    }
}