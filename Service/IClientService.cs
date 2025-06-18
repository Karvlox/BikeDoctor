namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClientsAsync();    
    Task<Client> GetClientByCIAsync(int ci);
    Task<Client> GetClientByPhoneAsync(int phoneNumber);
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);    
    Task DeleteClientAsync(int ci);
}