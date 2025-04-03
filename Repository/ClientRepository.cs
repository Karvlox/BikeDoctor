using BikeDoctor.Repository;
using Microsoft.EntityFrameworkCore;
using BikeDoctor.Data;
using BikeDoctor.Models;
namespace BankDB.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly BikeDoctorContext _context;

    public ClientRepository(BikeDoctorContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client> GetClientByIdAsync(Guid id)
    {
        return await _context.Clients.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Client> GetClientByCIAsync(int ci)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.CI == ci);
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

    public async Task DeleteClientAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteClientByCIAsync(int ci)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.CI == ci);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}