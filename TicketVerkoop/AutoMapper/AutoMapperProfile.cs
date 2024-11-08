﻿using AutoMapper;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //--------- Match ---------------
        CreateMap<Match, MatchVM>()
            .ForMember(dest => dest.StadiumNaam, 
            opts => opts.MapFrom(src => src.Stadium.Naam))
            .ForMember(dest => dest.ThuisPloegNaam,
            opts => opts.MapFrom(src => src.PloegThuis.Naam))
            .ForMember(dest => dest.UitPloegNaam,
            opts => opts.MapFrom(src => src.PloegUit.Naam))
            .ForMember(dest => dest.Datum,
            opts => opts.MapFrom(src => src.Datum.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.DayOfWeek,
            opts => opts.MapFrom(src => src.Datum.DayOfWeek))
            .ForMember(dest => dest.Time,
            opts => opts.MapFrom(src => src.Datum.ToString("HH:mm")))
            .ForMember(dest => dest.DateTime,
            opts => opts.MapFrom(src => src.Datum));
        CreateMap<MatchVM, Match>();

        CreateMap<Stadium, StadiumVM>();
        CreateMap<StadiumVM, Stadium>();

        CreateMap<Ploeg, PloegVM>()
            .ForMember(dest => dest.StadiumNaam,
            opts => opts.MapFrom(src => src.ThuisStadium.Naam));
        CreateMap<PloegVM, Ploeg>();

        //--------- Voor Swagger ---------------
        CreateMap<Match, MatchSwaggerVM>();

        //--------- Voor Tickets ---------------
        CreateMap<Stadium, StadiumTicketVM>();

        CreateMap<Match, StadiumTicketVM>()
            .ForMember(dest => dest.MatchId,
                opts => opts.MapFrom(src => src.MatchId))
            .ForMember(dest => dest.Rings,
                opts => opts.MapFrom(src => src.Stadium.Rings))
            .ForMember(dest => dest.Stad,
                opts => opts.MapFrom(src => src.Stadium.Stad))
            .ForMember(dest => dest.Sections,
                opts => opts.MapFrom(src => src.Stadium.Rings.SelectMany(r => r.Sections)))
            .ForMember(dest => dest.ThuisPloegNaam,
                opts => opts.MapFrom(src => src.PloegThuis.Naam))
            .ForMember(dest => dest.UitPloegNaam,
                opts => opts.MapFrom(src => src.PloegUit.Naam))
            .ForMember(dest => dest.Datum,
                opts => opts.MapFrom(src => src.Datum.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.DayOfWeek,
                opts => opts.MapFrom(src => src.Datum.DayOfWeek))
            .ForMember(dest => dest.Time,
                opts => opts.MapFrom(src => src.Datum.ToString("HH:mm")));

        CreateMap<Bestelling, BestellingenVM>();
        CreateMap<BestelllingVM, Bestelling>();

        // -----------------------------------
        //CreateMap<Bestelling, BestellingenVM>();
        CreateMap<Abonnement, AbonnementSelectieVM>()
            .ForMember(dest => dest.PloegNaam,
                opts => opts.MapFrom(src => src.Ploeg.Naam))
            .ForMember(dest => dest.StadiumNaam,
                opts => opts.MapFrom(src => src.Ploeg.ThuisStadium.Naam))
            .ForMember(dest => dest.StoelId,
                opts => opts.MapFrom(src => src.Zitplaats.First().ZitplaatsId))
            .ForMember(dest => dest.SelectedSectiondId,
                opts => opts.MapFrom(src => src.Zitplaats.First().SectionId))
            .ForMember(dest => dest.SelectedRingNaam,
                opts => opts.MapFrom(src => src.Zitplaats.First().Section.Ring.ZoneLocatie));

        CreateMap<Ticket, TicketVM>()
            .ForMember(dest => dest.ThuisPloegNaam,
                opts => opts.MapFrom(src => src.Match.PloegThuis.Naam))
            .ForMember(dest => dest.UitPloegNaam,
                opts => opts.MapFrom(src => src.Match.PloegUit.Naam))
            .ForMember(dest => dest.StadiumNaam,
                opts => opts.MapFrom(src => src.Match.Stadium.Naam))
            .ForMember(dest => dest.SectionId,
                opts => opts.MapFrom(src => src.Zitplaats.Any() ? src.Zitplaats.First().SectionId : (int?)null))
            .ForMember(dest => dest.RingNaam,
                opts => opts.MapFrom(src => src.Zitplaats.Any() ? src.Zitplaats.First().Section.Ring.ZoneLocatie : null))
            .ForMember(dest => dest.aantaZitPlaatsen,
                opts => opts.MapFrom(src => src.Zitplaats.Count))
            .ForMember(dest => dest.Datum,
                opts => opts.MapFrom(src => src.Match.Datum.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.DayOfWeek,
                opts => opts.MapFrom(src => src.Match.Datum.DayOfWeek))
            .ForMember(dest => dest.Time,
                opts => opts.MapFrom(src => src.Match.Datum.ToString("HH:mm")))
            .ForMember(dest => dest.DateTime,
                opts => opts.MapFrom(src => src.Match.Datum));




        CreateMap<Zitplaat, AbonnementSelectieVM>()
             .ForMember(dest => dest.SelectedSectiondId,
                   opts => opts.MapFrom(src => src.SectionId));


        CreateMap<Ring, RingVM>();
        CreateMap<RingVM, Ring>();

        CreateMap<Section, SectionVM>();
        CreateMap<SectionVM, Section>();

        CreateMap<Zitplaat, ZitPlaatsVM>();
        CreateMap<ZitPlaatsVM, Zitplaat>();

        CreateMap<AbonnementSelectieVM, Abonnement>();
        CreateMap<AbonnementSelectieVM, Zitplaat>()
            .ForMember(dest => dest.SectionId,
                opts => opts.MapFrom(src => src.SelectedSectiondId));

        CreateMap<TicketVM, Ticket>();
        CreateMap<TicketVM, Zitplaat>();

        CreateMap<AspNetUser, UserVM>();
    }
}
