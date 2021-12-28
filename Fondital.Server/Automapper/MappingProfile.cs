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

            CreateMap<Ricambio, RicambioDto>()
                .ForMember(dest => dest.Amount, src => src.MapFrom(s => s.Costo));
            CreateMap<RicambioDto, Ricambio>()
                .ForMember(dest => dest.Costo, src => src.MapFrom(s => s.Amount));

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

            CreateMap<ServicePartnerResponseDto, ServicePartnerDto>()
                .ForMember(dest => dest.ContractDate, src => src.MapFrom(s => GetDateTime(s.ContractDate)));
        }

        private DateTime? GetDateTime(string contractDate)
        {
            return DateTime.TryParseExact(contractDate, "yyyyMMdd", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var date) ? date : (DateTime?) null;
        }
    }
}