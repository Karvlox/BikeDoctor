using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    public async Task<ActionResult<IEnumerable<Motorcycle>>> GetMotorcycles()
    {
        var motorcycles = await _motorcycleService.GetAllMotorcyclesAsync();
        return Ok(motorcycles);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Motorcycle>> GetMotorcycleById(Guid id)
    {
        var motorcycle = await _motorcycleService.GetMotorcycleByIdAsync(id);
        if (motorcycle == null)
        {
            return NotFound();
        }
        return Ok(motorcycle);
    }

    [HttpGet("licensePlate/{licensePlate}")]
    [Authorize]
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
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<ActionResult<Motorcycle>> CreateMotocycle([FromBody] Motorcycle motorcycle)
    {
        try
        {
            await _motorcycleService.AddMotorcycleAsync(motorcycle);
            return CreatedAtAction(nameof(GetMotorcycleById), new { id = motorcycle.Id }, motorcycle);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]

    public async Task<IActionResult> UpdateMotorcycle(Guid id, [FromBody] Motorcycle motorcycle)
    {
        try
        {
            if (id != motorcycle.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }
            await _motorcycleService.UpdateMotorcycletAsync(motorcycle);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("licensePlate/{licensePlate}")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> UpdateMotorcycleByLicensePlateNumber(string licensePlate, [FromBody] Motorcycle motorcycle)
    {
        try
        {
            if (licensePlate != motorcycle.LicensePlateNumber)
            {
                return BadRequest("El número de matrícula en la URL no coincide con el del cuerpo de la solicitud.");
            }
            await _motorcycleService.UpdateMotorcycleByLicensePlateNumberAsync(licensePlate, motorcycle);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("not found"))
            {
                return NotFound(ex.Message);
            }
            return StatusCode(500, "Ocurrió un error al actualizar la motocicleta.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMotorcycle(Guid id)
    {
        await _motorcycleService.DeleteMotorcycleAsync(id);
        return NoContent();
    }

    [HttpDelete("licensePlate/{licensePlate}")]
    public async Task<IActionResult> DeleteMotorcycleByLicensePlateNumber(string licensePlate)
    {
        var motorcycle = await _motorcycleService.GetMotorcycleByLicensePlateNumberAsync(licensePlate);
        if (motorcycle == null)
        {
            return NotFound($"No se encontró una motocicleta con el número de matrícula {licensePlate}.");
        }
        
        await _motorcycleService.DeleteMotorcycleByLicensePlateNumberAsync(licensePlate);
        return NoContent();
    }
}