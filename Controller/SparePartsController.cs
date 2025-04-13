namespace BikeDoctor.Controllers;

using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
    public async Task<ActionResult<IEnumerable<SpareParts>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("{id}")]
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
    public async Task<IActionResult> Update(Guid id, [FromBody] SpareParts spareParts)
    {
        try
        {
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