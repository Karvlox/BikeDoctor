namespace BikeDoctor.Controllers;

using BikeDoctor.Models;
using BikeDoctor.DTOs;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CostApprovalController : ControllerBase
{
    private readonly ICostApprovalService _service;

    public CostApprovalController(ICostApprovalService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostApproval>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var receptions = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(receptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostApproval>> GetById(Guid id)
    {
        try
        {
            var costApproval = await _service.GetByIdAsync(id);
            return Ok(costApproval);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CostApproval>> Create([FromBody] CostApproval costApproval)
    {
        try
        {
            await _service.AddAsync(costApproval);
            return CreatedAtAction(nameof(GetById), new { id = costApproval.Id }, costApproval);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCostApprovalDto costApprovalDto)
    {
        try
        {
            var costApproval = new CostApproval
            {
                Id = id,
                Date = costApprovalDto.Date,
                ClientCI = costApprovalDto.ClientCI,
                MotorcycleLicensePlate = costApprovalDto.MotorcycleLicensePlate,
                EmployeeCI = costApprovalDto.EmployeeCI,
                ListLaborCosts = costApprovalDto.ListLaborCosts,
                Reviewed = costApprovalDto.Reviewed,
            };

            await _service.UpdateAsync(id, costApproval);
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