using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models;

namespace Fondital.Shared.Services
{
    public interface ITraceService
    {
        Task<IEnumerable<Trace>> GetAllTraces();
        Task<Trace> GetTraceById(int id);
        Task<Trace> CreateTrace(Trace trace);
        Task UpdateTrace(Trace traceToBeUpdated, Trace trace);
        Task DeleteTrace(Trace trace);
    }
}