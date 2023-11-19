using API.Data.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class ComponentsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ComponentsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [Authorize]
    [HttpGet("{componentId}")]
    public async Task<ActionResult> GetComponentById(long componentId)
    {
        var component = await _unitOfWork.ComponentsRepository.GetById(componentId);
        if (component == null) return NotFound();

        return Ok(component);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddNewComponent(NewProductComponentDto newComponentData)
    {
        var newComponent = _mapper.Map<ProductComponent>(newComponentData);
        _unitOfWork.ComponentsRepository.Add(newComponent);

        if (await _unitOfWork.Complete()) return Ok(newComponent);

        return BadRequest("Issue adding component to database");
    }
    
    [HttpPut("{componentId}")]
    public async Task<ActionResult> UpdateComponent(long componentId, [FromBody] NewProductComponentDto updateComponentData)
    {
        var component = await _unitOfWork.ComponentsRepository.GetById(componentId);
        if (component == null) return NotFound();

        _mapper.Map(updateComponentData, component);

        if (await _unitOfWork.Complete()) return Ok(component);

        return BadRequest("Issue updating component to database");
    }
    
    [HttpDelete("{componentId}")]
    public async Task<ActionResult> UpdateComponent(long componentId)
    {
        var component = await _unitOfWork.ComponentsRepository.GetById(componentId);
        if (component == null) return NotFound();
        
        _unitOfWork.ComponentsRepository.Remove(component);

        if (await _unitOfWork.Complete()) return Ok();

        return BadRequest("Issue deleting component from database");
    }
}