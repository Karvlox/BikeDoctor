namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client> GetClientByIdAsync(Guid id);
    Task<Client> GetClientByCIAsync(int ci);
    Task<Client> GetClientByPhoneAsync(int phoneNumber);
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);
    Task DeleteClientAsync(Guid id);
    Task DeleteClientByCIAsync(int ci);
}