namespace BikeDoctor.Service
{
    using BikeDoctor.Models;
    using System;
    using System.Threading.Tasks;

    public interface IFlowAttentionService
    {
        Task<IEnumerable<FlowAttention>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<FlowAttention> GetByIdAsync(Guid id);
        Task<FlowAttention> GetByClientAndLicenseAsync(int clientCI, string licensePlate);
        Task<FlowAttention> GetByClientCIAsync(int clientCI);
        Task<FlowAttention> GetByEmployeeCIAsync(int employeeCI);
        Task AddAsync(FlowAttention flowAttention);
        Task UpdateAsync(Guid id, FlowAttention flowAttention);
        Task DeleteAsync(Guid id);
    }
}