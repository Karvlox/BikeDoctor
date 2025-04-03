namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client> GetClientByIdAsync(Guid id);
    Task<Client> GetClientByCIAsync(int ci);
    Task<Client> GetClientByPhoneAsync(int phoneNumber);
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);
    Task UpdateClientByCIAsync(int ci, Client client);
    Task DeleteClientAsync(Guid id);
    Task DeleteClientByCIAsync(int ci);
}