using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;

namespace Fondital.Server.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Difetto, DifettoDto>();
            CreateMap<DifettoDto, Difetto>();

            CreateMap<Lavorazione, LavorazioneDto>();
            CreateMap<LavorazioneDto, Lavorazione>();

            CreateMap<VoceCosto, VoceCostoDto>();
            CreateMap<VoceCostoDto, VoceCosto>();

            CreateMap<ServicePartner, ServicePartnerDto>();
            CreateMap<ServicePartnerDto, ServicePartner>();

            CreateMap<Listino, ListinoDto>();
            CreateMap<ListinoDto, Listino>();

            CreateMap<Utente, UtenteDto>();
            CreateMap<UtenteDto, Utente>();

            CreateMap<Ruolo, RuoloDto>();
            CreateMap<RuoloDto, Ruolo>();

            CreateMap<Rapporto, RapportoDto>();
            CreateMap<RapportoDto, Rapporto>();

            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>();

            CreateMap<Ricambio, RicambioDto>();
            CreateMap<RicambioDto, Ricambio>();

            CreateMap<Caldaia, CaldaiaDto>();
            CreateMap<CaldaiaDto, Caldaia>();

            CreateMap<RapportoVoceCosto, RapportoVoceCostoDto>();
            CreateMap<RapportoVoceCostoDto, RapportoVoceCosto>();
        }
    }
}