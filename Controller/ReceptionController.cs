namespace BikeDoctor.Controllers;

using System;
using System.Threading.Tasks;
using BikeDoctor.Models;
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
    public async Task<IActionResult> Update(Guid id, [FromBody] Reception reception)
    {
        try
        {
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
}
