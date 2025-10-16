using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BikeDoctor.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using BikeDoctor.DTOs;

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
        [Authorize]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var flowAttentions = await _flowAttentionService.GetAllAsync(pageNumber, pageSize);
            return Ok(flowAttentions);
        }

        [HttpGet("{id}")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "ADMIN,EMPLEADO")]
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
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<IActionResult> Update(Guid id, UpdateFlowAttentionDto flowAttentionDto)
        {
            try
            {
                var flowAttention = new FlowAttention
                {
                    Id = id,
                    ClientCI = flowAttentionDto.ClientCI,
                    EmployeeCI = flowAttentionDto.EmployeeCI,
                    ReceptionID = flowAttentionDto.ReceptionID,
                    DiagnosisID = flowAttentionDto.DiagnosisID,
                    SparePartsID = flowAttentionDto.SparePartsID,
                    CostApprovalID = flowAttentionDto.CostApprovalID,
                    RepairID = flowAttentionDto.RepairID,
                    QualityControlID = flowAttentionDto.QualityControlID,
                    DeliveryID = flowAttentionDto.DeliveryID,
                };

                await _flowAttentionService.UpdateAsync(id, flowAttention);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
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