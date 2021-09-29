﻿using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Services
{
    public interface IDifettoService
    {
        Task<IEnumerable<Difetto>> GetAllDifetti(bool? isAbilitato = null);
        Task<Difetto> GetDifettoById(int id);
        Task UpdateDifetto(int difettoId, Difetto difetto);
        Task<int> AddDifetto(Difetto difetto);
    }
}