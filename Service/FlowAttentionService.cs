namespace BikeDoctor.Service
{
    using BikeDoctor.Models;
    using BikeDoctor.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FlowAttentionService : IFlowAttentionService
    {
        private readonly IFlowAttentionRepository _repository;

        public FlowAttentionService(IFlowAttentionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FlowAttention>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _repository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<FlowAttention> GetByIdAsync(Guid id)
        {
            var flowAttention = await _repository.GetByIdAsync(id);
            if (flowAttention == null)
            {
                throw new KeyNotFoundException($"No se encontró un flujo de atención con ID {id}.");
            }
            return flowAttention;
        }

        public async Task<FlowAttention> GetByClientAndLicenseAsync(int clientCI, string licensePlate)
        {
            var flowAttention = await _repository.GetByClientAndLicenseAsync(clientCI, licensePlate);
            if (flowAttention == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un flujo de atención para el cliente CI {clientCI} y placa {licensePlate}."
                );
            }
            return flowAttention;
        }

        public async Task<FlowAttention> GetByClientCIAsync(int clientCI)
        {
            var flowAttention = await _repository.GetByClientCIAsync(clientCI);
            if (flowAttention == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un flujo de atención para el cliente CI {clientCI}."
                );
            }
            return flowAttention;
        }

        public async Task<FlowAttention> GetByEmployeeCIAsync(int employeeCI)
        {
            var flowAttention = await _repository.GetByEmployeeCIAsync(employeeCI);
            if (flowAttention == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró un flujo de atención para el empleado CI {employeeCI}."
                );
            }
            return flowAttention;
        }

        public async Task AddAsync(FlowAttention flowAttention)
        {
            if (flowAttention == null)
            {
                throw new ArgumentNullException(nameof(flowAttention));
            }
            await _repository.AddAsync(flowAttention);
        }

        public async Task UpdateAsync(Guid id, FlowAttention flowAttention)
        {
            if (flowAttention == null)
            {
                throw new ArgumentNullException(nameof(flowAttention));
            }
            await _repository.UpdateAsync(flowAttention);
        }

        public async Task DeleteAsync(Guid id)
        {
            var flowAttention = await _repository.GetByIdAsync(id);
            if (flowAttention == null)
            {
                throw new KeyNotFoundException($"No se encontró un flujo de atención con ID {id}.");
            }
            await _repository.DeleteAsync(id);
        }
    }
}