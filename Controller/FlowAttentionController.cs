using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BikeDoctor.Models;
using System;

namespace BikeDoctor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowAttentionController : ControllerBase
    {
        private readonly IFlowAttentionService _flowAttentionService;

        public FlowAttentionController(IFlowAttentionService flowAttentionService)
        {
            _flowAttentionService = flowAttentionService ?? throw new ArgumentNullException(nameof(flowAttentionService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var flowAttentions = await _flowAttentionService.GetAllAsync(pageNumber, pageSize);
            return Ok(flowAttentions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var flowAttention = await _flowAttentionService.GetByIdAsync(id);
                return Ok(flowAttention);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-client-license")]
        public async Task<IActionResult> GetByClientAndLicense(int clientCI, string licensePlate)
        {
            try
            {
                var flowAttention = await _flowAttentionService.GetByClientAndLicenseAsync(clientCI, licensePlate);
                return Ok(flowAttention);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-client")]
        public async Task<IActionResult> GetByClientCI(int clientCI)
        {
            try
            {
                var flowAttention = await _flowAttentionService.GetByClientCIAsync(clientCI);
                return Ok(flowAttention);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-employee")]
        public async Task<IActionResult> GetByEmployeeCI(int employeeCI)
        {
            try
            {
                var flowAttention = await _flowAttentionService.GetByEmployeeCIAsync(employeeCI);
                return Ok(flowAttention);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(FlowAttention flowAttention)
        {
            if (flowAttention == null)
            {
                return BadRequest(new { message = "El flujo de atención no puede ser nulo." });
            }
            try
            {
                await _flowAttentionService.AddAsync(flowAttention);
                return CreatedAtAction(nameof(GetById), new { id = flowAttention.Id }, flowAttention);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al agregar el flujo de atención.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, FlowAttention flowAttention)
        {
            if (flowAttention == null || id != flowAttention.Id)
            {
                return BadRequest(new { message = "Los datos proporcionados no son válidos." });
            }
            try
            {
                await _flowAttentionService.UpdateAsync(flowAttention);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el flujo de atención.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _flowAttentionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el flujo de atención.", details = ex.Message });
            }
        }
    }
}