namespace BikeDoctor.Repository;

using BikeDoctor.Models;

public interface IMotorcycleRepository
{
    Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync();
    Task<Motorcycle> GetMotorcyclesByIdAsync(Guid id);
    Task<Motorcycle> GetMotorcycleByLicensePlateNumberAsync(string LicensePlateNumber);
    Task AddMotorcycleAsync(Motorcycle mtorcycle);
    Task UpdateMotorcycleAsync(Motorcycle motorcycle);
    Task DeleteMotorcycleAsync(Guid id);
    Task DeleteMotorcycleByLicensePlateNumberAsync(string licensePlateNumber);
}