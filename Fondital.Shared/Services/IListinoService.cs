using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models;

namespace Fondital.Shared.Services
{
    public interface IListinoService
    {
        Task<IEnumerable<Listino>> GetAllListini();
        Task<Listino> GetListinoById(int id);
        Task UpdateListino(int listinoId, Listino listino);
        Task AddListino(Listino listino);
    }
}