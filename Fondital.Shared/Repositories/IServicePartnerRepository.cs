using Fondital.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IServicePartnerRepository : IRepository<ServicePartner>
    {
        Task<ServicePartner> GetByIdWithUtenti(int id);
    }
}