namespace BikeDoctor.Repository
{
    using BikeDoctor.Models;
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IFlowAttentionRepository
    {
        Task<IEnumerable<FlowAttention>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<FlowAttention?> GetByIdAsync(Guid id);
        Task<FlowAttention?> GetByClientAndLicenseAsync(int clientCI, string licensePlate);
        Task<FlowAttention?> GetByClientCIAsync(int clientCI);
        Task<FlowAttention?> GetByEmployeeCIAsync(int employeeCI);
        Task AddAsync(FlowAttention flowAttention);
        Task UpdateAsync(FlowAttention flowAttention);
        Task DeleteAsync(Guid id);
    }
}