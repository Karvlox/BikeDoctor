namespace BikeDoctor.Controllers;

using System;
using System.Threading.Tasks;
using BikeDoctor.Models;
using BikeDoctor.DTOs;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class RepairController : ControllerBase
{
    private readonly IRepairService _service;

    public RepairController(IRepairService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Repair>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Repair>> GetById(Guid id)
    {
        try
        {
            var repair = await _service.GetByIdAsync(id);
            return Ok(repair);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<ActionResult<Repair>> Create([FromBody] Repair repair)
    {
        try
        {
            await _service.AddAsync(repair);
            return CreatedAtAction(nameof(GetById), new { id = repair.Id }, repair);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRepairDto repairDto)
    {
        try
        {
            var repair = new Repair
            {
                Id = id,
                Date = repairDto.Date,
                ClientCI = repairDto.ClientCI,
                MotorcycleLicensePlate = repairDto.MotorcycleLicensePlate,
                EmployeeCI = repairDto.EmployeeCI,
                ListReparations = repairDto.ListReparations,
                Reviewed = repairDto.Reviewed,
            };

            await _service.UpdateAsync(id, repair);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id}/reviewed")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> UpdateReviewedStatus(Guid id, [FromQuery] bool reviewed)
    {
        try
        {
            var repair = await _service.GetByIdAsync(id);
            repair.Reviewed = reviewed;

            await _service.UpdateAsync(id, repair);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error updating Reviewed status: {ex.Message}");
        }
    }
}
