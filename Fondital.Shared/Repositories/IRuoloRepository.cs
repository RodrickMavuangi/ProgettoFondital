﻿using Fondital.Shared.Dto;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IRuoloRepository : IRepository<Ruolo>
    {
        Task<IEnumerable<Ruolo>> GetAll();
    }
}