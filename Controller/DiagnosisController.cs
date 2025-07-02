namespace BikeDoctor.Controllers;

using System;
using System.Threading.Tasks;
using BikeDoctor.DTOs;
using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class DiagnosisController : ControllerBase
{
    private readonly IDiagnosisService _service;

    public DiagnosisController(IDiagnosisService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Diagnosis>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var diagnosis = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(diagnosis);
    }

    [HttpGet("by-employee/{employeeCI}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Diagnosis>>> GetAllByEmployeeCI(
        int employeeCI,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var diagnosis = await _service.GetAllByEmployeeCIAsync(employeeCI, pageNumber, pageSize);
        return Ok(diagnosis);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Diagnosis>> GetById(Guid id)
    {
        try
        {
            var diagnosis = await _service.GetByIdAsync(id);
            return Ok(diagnosis);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<ActionResult<Diagnosis>> Create([FromBody] Diagnosis diagnosis)
    {
        try
        {
            await _service.AddAsync(diagnosis);
            return CreatedAtAction(nameof(GetById), new { id = diagnosis.Id }, diagnosis);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDiagnosisDto diagnosisDto)
    {
        try
        {
            var diagnosis = new Diagnosis
            {
                Id = id,
                Date = diagnosisDto.Date,
                ClientCI = diagnosisDto.ClientCI,
                MotorcycleLicensePlate = diagnosisDto.MotorcycleLicensePlate,
                EmployeeCI = diagnosisDto.EmployeeCI,
                ListDiagnostics = diagnosisDto.ListDiagnostics,
                Reviewed = diagnosisDto.Reviewed,
            };

            await _service.UpdateAsync(id, diagnosis);
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
            var diagnosis = await _service.GetByIdAsync(id);
            diagnosis.Reviewed = reviewed;

            await _service.UpdateAsync(id, diagnosis);

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
