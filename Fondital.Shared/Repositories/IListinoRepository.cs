using Fondital.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IListinoRepository : IRepository<Listino>
    {
        Task<Listino> GetListinoByIdAsync(int Id);
    }
}