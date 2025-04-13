namespace BikeDoctor.Controllers;

using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
    public async Task<ActionResult<IEnumerable<Diagnosis>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("{id}")]
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
    public async Task<IActionResult> Update(Guid id, [FromBody] Diagnosis diagnosis)
    {
        try
        {
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
}