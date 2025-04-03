using BikeDoctor.Models;
using BikeDoctor.Service;
using Microsoft.AspNetCore.Mvc;

namespace BikeDoctor.Controller;


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
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClientById(Guid id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    [HttpGet("ci/{ci}")]
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
    public async Task<ActionResult<Client>> CreateClient([FromBody] Client client)
    {
        await _clientService.AddClientAsync(client);
        return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(Guid id, [FromBody] Client client)
    {
        if (id != client.Id)
        {
            return BadRequest();
        }
        await _clientService.UpdateClientAsync(client);
        return NoContent();
    }

    [HttpPut("ci/{ci}")]
    public async Task<IActionResult> UpdateClientByCI(int ci, [FromBody] Client client)
    {
        try
        {
            if (ci != client.CI)
            {
                return BadRequest("CI in route must match CI in request body");
            }

            await _clientService.UpdateClientByCIAsync(ci, client);
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("not found"))
            {
                return NotFound(ex.Message);
            }
            return StatusCode(500, "An error occurred while updating the client");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        await _clientService.DeleteClientAsync(id);
        return NoContent();
    }

    [HttpDelete("ci/{ci}")]
    public async Task<IActionResult> DeleteClientByCI(int ci)
    {
        var client = await _clientService.GetClientByCIAsync(ci);
        if (client == null)
        {
            return NotFound($"Client with CI {ci} not found");
        }
        
        await _clientService.DeleteClientByCIAsync(ci);
        return NoContent();
    }
}