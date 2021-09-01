using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;

namespace Fondital.Services
{
    public class TraceService : ITraceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TraceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Trace> CreateTrace(Trace newTrace)
        {
            await _unitOfWork.Traces
                .AddAsync(newTrace);
            await _unitOfWork.CommitAsync();

            return newTrace;
        }

        public async Task DeleteTrace(Trace trace)
        {
            _unitOfWork.Traces.Remove(trace);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Trace>> GetAllTraces()
        {
            return await _unitOfWork.Traces.GetAllAsync();
        }

        public async Task<Trace> GetTraceById(int id)
        {
            return await _unitOfWork.Traces.GetByIdAsync(id);
        }

        public async Task UpdateTrace(Trace traceToUpdate, Trace trace)
        {
            _unitOfWork.Update(traceToUpdate, trace);

            await _unitOfWork.CommitAsync();
        }
    }
}