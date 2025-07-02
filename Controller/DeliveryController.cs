namespace BikeDoctor.Controllers;

using System;
using System.Threading.Tasks;
using BikeDoctor.DTOs;
using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _service;

    public DeliveryController(IDeliveryService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Delivery>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("by-employee/{employeeCI}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Delivery>>> GetAllByEmployeeCI(
        int employeeCI,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllByEmployeeCIAsync(employeeCI, pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Delivery>> GetById(Guid id)
    {
        try
        {
            var delivery = await _service.GetByIdAsync(id);
            return Ok(delivery);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<ActionResult<Delivery>> Create([FromBody] Delivery delivery)
    {
        try
        {
            await _service.AddAsync(delivery);
            return CreatedAtAction(nameof(GetById), new { id = delivery.Id }, delivery);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDeliveryDto deliveryDto)
    {
        try
        {
            var delivery = new Delivery
            {
                Id = id,
                Date = deliveryDto.Date,
                ClientCI = deliveryDto.ClientCI,
                MotorcycleLicensePlate = deliveryDto.MotorcycleLicensePlate,
                EmployeeCI = deliveryDto.EmployeeCI,
                SurveyCompleted = deliveryDto.SurveyCompleted,
                Reviewed = deliveryDto.Reviewed,
            };

            await _service.UpdateAsync(id, delivery);
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
            var delivery = await _service.GetByIdAsync(id);
            delivery.Reviewed = reviewed;

            await _service.UpdateAsync(id, delivery);

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

    [HttpPatch("{id}/surveyCompleted")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> UpdateSurveyCompletedStatus(
        Guid id,
        [FromQuery] bool surveyCompleted
    )
    {
        try
        {
            var delivery = await _service.GetByIdAsync(id);
            delivery.SurveyCompleted = surveyCompleted;

            await _service.UpdateAsync(id, delivery);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error updating Survey Completed status: {ex.Message}");
        }
    }
}
