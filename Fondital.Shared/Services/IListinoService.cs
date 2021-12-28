using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Services
{
    public interface IListinoService
    {
        Task<IEnumerable<Listino>> GetAllListini();
        Task<Listino> GetListinoById(int id);
        Task UpdateListino(int listinoId, Listino listino);
        Task<int> AddListino(Listino listino);
    }
}