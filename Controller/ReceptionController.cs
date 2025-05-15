namespace BikeDoctor.Controllers;

using System;
using System.Threading.Tasks;
using BikeDoctor.Models;
using BikeDoctor.DTOs;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReceptionController : ControllerBase
{
    private readonly IReceptionService _service;

    public ReceptionController(IReceptionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reception>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reception>> GetById(Guid id)
    {
        try
        {
            var reception = await _service.GetByIdAsync(id);
            return Ok(reception);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Reception>> Create([FromBody] Reception reception)
    {
        try
        {
            await _service.AddAsync(reception);
            return CreatedAtAction(nameof(GetById), new { id = reception.Id }, reception);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReceptionDto receptionDto)
    {
        try
        {
            var reception = new Reception
            {
                Id = id,
                Date = receptionDto.Date,
                ClientCI = receptionDto.ClientCI,
                MotorcycleLicensePlate = receptionDto.MotorcycleLicensePlate,
                EmployeeCI = receptionDto.EmployeeCI,
                Reasons = receptionDto.Reasons,
                Images = receptionDto.Images,
                Reviewed = receptionDto.Reviewed,
            };

            await _service.UpdateAsync(id, reception);
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
    public async Task<IActionResult> UpdateReviewedStatus(Guid id, [FromQuery] bool reviewed)
    {
        try
        {
            var reception = await _service.GetByIdAsync(id);
            reception.Reviewed = reviewed;

            await _service.UpdateAsync(id, reception);

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
