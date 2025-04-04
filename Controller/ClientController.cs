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

    [HttpGet("{ci}")]
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
        if (client.CI <= 7)
        {
            return BadRequest("El CI es obligatorio y debe ser un valor válido.");
        }

        await _clientService.AddClientAsync(client);
        return CreatedAtAction(nameof(GetClientByCI), new { ci = client.CI }, client);
    }

    [HttpPut("{ci}")]
    public async Task<IActionResult> UpdateClient(int ci, [FromBody] Client client)
    {
        if (ci != client.CI)
        {
            return BadRequest();
        }
        await _clientService.UpdateClientAsync(client);
        return NoContent();
    }

    [HttpDelete("{ci}")]
    public async Task<IActionResult> DeleteClient(int ci)
    {
        await _clientService.DeleteClientAsync(ci);
        return NoContent();
    }
}