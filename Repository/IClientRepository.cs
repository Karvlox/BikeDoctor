namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client> GetClientByCIAsync(int ci);
    Task<Client> GetClientByPhoneAsync(int phoneNumber);
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);
    Task DeleteClientAsync(int ci);    
}