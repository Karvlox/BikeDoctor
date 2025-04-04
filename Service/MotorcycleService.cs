namespace BikeDoctor.Service;

using BikeDoctor.Models;
using BikeDoctor.Repository;

public class MotorcycleService : IMotorcycleService
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public MotorcycleService(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync()
    {
        return await _motorcycleRepository.GetAllMotorcyclesAsync();
    }

    public async Task<Motorcycle> GetMotorcycleByIdAsync(Guid id)
    {
        return await _motorcycleRepository.GetMotorcyclesByIdAsync(id);
    }

    public async Task<Motorcycle> GetMotorcycleByLicensePlateNumberAsync(string licensePlateNumber)
    {
        return await _motorcycleRepository.GetMotorcycleByLicensePlateNumberAsync(licensePlateNumber);
    }

    public async Task AddMotorcycleAsync(Motorcycle motorcycle)
    {
        await _motorcycleRepository.AddMotorcycleAsync(motorcycle);
    }

    public async Task UpdateMotorcycletAsync(Motorcycle motorcycle)
    {
        await _motorcycleRepository.UpdateMotorcycleAsync(motorcycle);
    }

    public async Task UpdateMotorcycleByLicensePlateNumberAsync(string licensePlateNumber, Motorcycle motorcycle)
    {
        var existingMotorcycle = await _motorcycleRepository.GetMotorcycleByLicensePlateNumberAsync(licensePlateNumber);
        if (existingMotorcycle == null)
        {
            throw new Exception($"Motorcycle with License Plate Number {licensePlateNumber} not found");
        }
        
        existingMotorcycle.Brand = motorcycle.Brand;
        existingMotorcycle.Model = motorcycle.Model;
        existingMotorcycle.LicensePlateNumber = motorcycle.LicensePlateNumber;
        existingMotorcycle.Color = motorcycle.Color;

        await _motorcycleRepository.UpdateMotorcycleAsync(existingMotorcycle);
    }

    public async Task DeleteMotorcycleAsync(Guid id)
    {
        await _motorcycleRepository.DeleteMotorcycleAsync(id);
    }

    public async Task DeleteMotorcycleByLicensePlateNumberAsync(string licensePlateNumber)
    {
        await _motorcycleRepository.DeleteMotorcycleByLicensePlateNumberAsync(licensePlateNumber);
    }
}