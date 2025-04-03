namespace BikeDoctor.Service;

using BikeDoctor.Repository;
using BikeDoctor.Models;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllClientsAsync();
    }

    public async Task<Client> GetClientByIdAsync(Guid id)
    {
        return await _clientRepository.GetClientByIdAsync(id);
    }

    public async Task<Client> GetClientByCIAsync(int ci)
    {
        return await _clientRepository.GetClientByCIAsync(ci);
    }

    public async Task<Client> GetClientByPhoneAsync(int phoneNumber)
    {
        return await _clientRepository.GetClientByPhoneAsync(phoneNumber);
    }

    public async Task AddClientAsync(Client client)
    {
        await _clientRepository.AddClientAsync(client);
    }

    public async Task UpdateClientAsync(Client client)
    {
        await _clientRepository.UpdateClientAsync(client);
    }

    public async Task UpdateClientByCIAsync(int ci, Client client)
    {
        var existingClient = await _clientRepository.GetClientByCIAsync(ci);
        if (existingClient == null)
        {
            throw new Exception($"Client with CI {ci} not found");
        }
        
        existingClient.Name = client.Name;
        existingClient.LastName = client.LastName;
        existingClient.CI = client.CI;
        existingClient.Age = client.Age;
        existingClient.NumberPhone = client.NumberPhone;
        existingClient.Gender = client.Gender;

        await _clientRepository.UpdateClientAsync(existingClient);
    }

    public async Task DeleteClientAsync(Guid id)
    {
        await _clientRepository.DeleteClientAsync(id);
    }

    public async Task DeleteClientByCIAsync(int ci)
    {
        await _clientRepository.DeleteClientByCIAsync(ci);
    }
}