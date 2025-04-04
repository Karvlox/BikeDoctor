using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;

namespace BikeDoctor.Controller;


[Route("api/[controller]")]
[ApiController]
public class MotorcycleController : ControllerBase
{
    private readonly IMotorcycleService _motorcycleService;

    public MotorcycleController(IMotorcycleService motorcycleService)
    {
        _motorcycleService = motorcycleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Motorcycle>>> GetMotorcycles()
    {
        var motorcycles = await _motorcycleService.GetAllMotorcyclesAsync();
        return Ok(motorcycles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModelBinderAttribute>> GetMotorcycleById(Guid id)
    {
        var motorcycle = await _motorcycleService.GetMotorcycleByIdAsync(id);
        if (motorcycle == null)
        {
            return NotFound();
        }
        return Ok(motorcycle);
    }

    [HttpGet("licensePlate/{licensePlate}")]
    public async Task<ActionResult<Motorcycle>> GetMotorcycleByLicensePlateNumber(string licensePlate)
    {
        var motorcycle = await _motorcycleService.GetMotorcycleByLicensePlateNumberAsync(licensePlate);
        if (motorcycle == null)
        {
            return NotFound();
        }
        return Ok(motorcycle);
    }

    [HttpPost]
    public async Task<ActionResult<Motorcycle>> CreateMotocycle([FromBody] Motorcycle motorcycle)
    {
        await _motorcycleService.AddMotorcycleAsync(motorcycle);
        return CreatedAtAction(nameof(GetMotorcycleById), new { id = motorcycle.Id }, motorcycle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMotorcycle(Guid id, [FromBody] Motorcycle motorcycle)
    {
        if (id != motorcycle.Id)
        {
            return BadRequest();
        }
        await _motorcycleService.UpdateMotorcycletAsync(motorcycle);
        return NoContent();
    }

    [HttpPut("licensePlate/{licensePlate}")]
    public async Task<IActionResult> UpdateMotorcycleByLicensePlateNumber(string licensePlate, [FromBody] Motorcycle motorcycle)
    {
        try
        {
            if (licensePlate != motorcycle.LicensePlateNumber)
            {
                return BadRequest("License Plate Number in route must match License Plate Number in request body");
            }

            await _motorcycleService.UpdateMotorcycleByLicensePlateNumberAsync(licensePlate, motorcycle);
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("not found"))
            {
                return NotFound(ex.Message);
            }
            return StatusCode(500, "An error occurred while updating the motorcyle");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMotorcycle(Guid id)
    {
        await _motorcycleService.DeleteMotorcycleAsync(id);
        return NoContent();
    }

    [HttpDelete("licensePlate/{licensePlate}")]
    public async Task<IActionResult> DeleteMotorcycleByLicensePlateNumber(string licensePlate)
    {
        var motorycle = await _motorcycleService.GetMotorcycleByLicensePlateNumberAsync(licensePlate);
        if (motorycle == null)
        {
            return NotFound($"Motorcycle with license plate number {licensePlate} not found");
        }
        
        await _motorcycleService.DeleteMotorcycleByLicensePlateNumberAsync(licensePlate);
        return NoContent();
    }
}