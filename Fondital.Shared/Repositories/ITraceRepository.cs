using Fondital.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface ITraceRepository : IRepository<Trace>
    {
        Task<IEnumerable<Trace>> GetAllWithUtenteAsync();
        Task<Trace> GetWithUtenteByIdAsync(int traceID);
        Task<IEnumerable<Trace>> GetAllWithUtenteByUtenteIdAsync(int utenteID);
    }
}