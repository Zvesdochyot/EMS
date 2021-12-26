using System;
using EMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace EMS.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PositionController : ControllerBase
{
    private readonly PositionService _positionService;

    public PositionController(PositionService positionService)
    {
        _positionService = positionService;
    }

    [HttpPost]
    public async Task<ActionResult<Position>> CreatePosition(Position position)
    {
        await _positionService.CreateAsync(position);
        return CreatedAtRoute("GetPosition", new { id = position.Id }, position);
    }

    [HttpGet]
    public async Task<ActionResult<List<Position>>> GetPositions() => await _positionService.GetAllAsync();

    [HttpGet("{id:length(24):guid}")]
    public async Task<ActionResult<Position>> GetPosition(Guid id)
    {
        var position = await _positionService.GetByIdAsync(id);
        return position != null ? position : NotFound();
    }
}
