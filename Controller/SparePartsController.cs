namespace BikeDoctor.Controllers;

using BikeDoctor.Models;
using BikeDoctor.DTOs;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class SparePartsController : ControllerBase
{
    private readonly ISparePartsService _service;

    public SparePartsController(ISparePartsService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<SpareParts>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var spareParts = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(spareParts);
    }

    [HttpGet("by-employee/{employeeCI}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<SpareParts>>> GetAllByEmployeeCI(
        int employeeCI,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var spareParts = await _service.GetAllByEmployeeCIAsync(employeeCI, pageNumber, pageSize);
        return Ok(spareParts);
    }


    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<SpareParts>> GetById(Guid id)
    {
        try
        {
            var spareParts = await _service.GetByIdAsync(id);
            return Ok(spareParts);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<ActionResult<SpareParts>> Create([FromBody] SpareParts spareParts)
    {
        try
        {
            await _service.AddAsync(spareParts);
            return CreatedAtAction(nameof(GetById), new { id = spareParts.Id }, spareParts);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSparePartsDto sparePartsDto)
    {
        try
        {
            var spareParts = new SpareParts
            {
                Id = id,
                Date = sparePartsDto.Date,
                ClientCI = sparePartsDto.ClientCI,
                MotorcycleLicensePlate = sparePartsDto.MotorcycleLicensePlate,
                EmployeeCI = sparePartsDto.EmployeeCI,
                ListSpareParts = sparePartsDto.ListSpareParts,
                Reviewed = sparePartsDto.Reviewed
            };

            await _service.UpdateAsync(id, spareParts);
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
            var spareParts = await _service.GetByIdAsync(id);
            spareParts.Reviewed = reviewed;

            await _service.UpdateAsync(id, spareParts);

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