using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using System;
using System.Globalization;

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

            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();

            CreateMap<Group, GroupDto>();
            CreateMap<GroupDto, Group>();

            CreateMap<CaldaiaResponseDto, CaldaiaDto>()
                .ForMember(dest => dest.Brand, src => src.MapFrom(s => s.Brand))
                .ForMember(dest => dest.Group, src => src.MapFrom(s => s.Group))
                .ForMember(dest => dest.Manufacturer, src => src.MapFrom(s => s.Manufacturer))
                .ForMember(dest => dest.Model, src => src.MapFrom(s => s.Model))
                .ForMember(dest => dest.ManufacturingDate, src => src.MapFrom(s => DateTime.ParseExact(s.ManufacturingDate, "yyyyMMdd", CultureInfo.InvariantCulture)));
        }
    }
}