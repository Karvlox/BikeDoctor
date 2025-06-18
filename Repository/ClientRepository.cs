using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using BikeDoctor.Data;
using BikeDoctor.Models;
namespace BikeDoctor.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly BikeDoctorContext _context;

    public ClientRepository(BikeDoctorContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients
            .Include(c => c.Motorcycles)
            .ToListAsync();
    }

    public async Task<Client> GetClientByCIAsync(int ci)
    {
        return await _context.Clients
            .Include(c => c.Motorcycles) // Incluye las motocicletas asociadas
            .FirstOrDefaultAsync(r => r.CI == ci);
    }

    public async Task<Client> GetClientByPhoneAsync(int phoneNumber)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.NumberPhone == phoneNumber);
    }

    public async Task AddClientAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClientAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClientAsync(int ci)
    {
        var client = await _context.Clients.FindAsync(ci);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}