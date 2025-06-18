namespace BikeDoctor.Controller;

using BikeDoctor.Models;
using BikeDoctor.Service;
using BikeDoctor.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{ci}")]
    [Authorize]
    public async Task<ActionResult<Client>> GetClientByCI(int ci)
    {
        var client = await _clientService.GetClientByCIAsync(ci);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    [HttpGet("phone/{phoneNumber}")]
    [Authorize]
    public async Task<ActionResult<Client>> GetClientByPhone(int phoneNumber)
    {
        var client = await _clientService.GetClientByPhoneAsync(phoneNumber);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<ActionResult<Client>> CreateClient([FromBody] Client client)
    {
        try
        {
            await _clientService.AddClientAsync(client);
            return CreatedAtAction(nameof(GetClientByCI), new { ci = client.CI }, client);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{ci}")]
    [Authorize(Roles = "ADMIN,EMPLEADO")]
    public async Task<IActionResult> UpdateClient(int ci, [FromBody] Client client)
    {
        try
        {
            if (ci != client.CI)
            {
                return BadRequest("El CI de la URL no coincide con el del cuerpo de la solicitud.");
            }
            await _clientService.UpdateClientAsync(client);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{ci}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> DeleteClient(int ci)
    {
        await _clientService.DeleteClientAsync(ci);
        return NoContent();
    }
}