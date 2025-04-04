namespace BikeDoctor.Service;

using BikeDoctor.Models;

public interface IMotorcycleService
{
    Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync();
    Task<Motorcycle> GetMotorcycleByIdAsync(Guid id);
    Task<Motorcycle> GetMotorcycleByLicensePlateNumberAsync(string licensePlateNumber);
    Task AddMotorcycleAsync(Motorcycle motorcycle);
    Task UpdateMotorcycletAsync(Motorcycle motorcycle);
    Task UpdateMotorcycleByLicensePlateNumberAsync(string licensePlateNumber, Motorcycle motorcycle);
    Task DeleteMotorcycleAsync(Guid id);
    Task DeleteMotorcycleByLicensePlateNumberAsync(string licensePlateNumber);
}