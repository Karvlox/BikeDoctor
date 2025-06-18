namespace BikeDoctor.Service;

using BikeDoctor.Repository;
using BikeDoctor.Models;
using BikeDoctor.Validations;

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
        ClientValidator.ValidateClientForAddOrUpdate(client);
        await _clientRepository.AddClientAsync(client);
    }

    public async Task UpdateClientAsync(Client client)
    {
        ClientValidator.ValidateClientForAddOrUpdate(client);
        await _clientRepository.UpdateClientAsync(client);
    }

    public async Task DeleteClientAsync(int ci)
    {
        await _clientRepository.DeleteClientAsync(ci);
    }
}