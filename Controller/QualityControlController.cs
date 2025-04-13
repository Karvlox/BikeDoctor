namespace BikeDoctor.Controllers;

using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class QualityControlController : ControllerBase
{
    private readonly IQualityControlService _service;

    public QualityControlController(IQualityControlService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QualityControl>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QualityControl>> GetById(Guid id)
    {
        try
        {
            var qualityControl = await _service.GetByIdAsync(id);
            return Ok(qualityControl);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<QualityControl>> Create([FromBody] QualityControl qualityControl)
    {
        try
        {
            await _service.AddAsync(qualityControl);
            return CreatedAtAction(nameof(GetById), new { id = qualityControl.Id }, qualityControl);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] QualityControl qualityControl)
    {
        try
        {
            await _service.UpdateAsync(id, qualityControl);
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