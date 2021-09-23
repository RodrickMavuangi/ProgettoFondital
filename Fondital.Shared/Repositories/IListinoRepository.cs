using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IListinoRepository : IRepository<Listino>
    {
        Task<List<Listino>> GetAllListiniAsync();
        Task<Listino> GetListinoByIdAsync(int Id);
        Task AddListino(Listino listino);
    }
}