using AutoMapper;
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
            opts => opts.MapFrom(src => src.Datum.ToString("HH:mm")));
        CreateMap<MatchVM, Match>();

        //--------- Stadium ---------------
        CreateMap<Stadium, StadiumVM>();
        CreateMap<StadiumVM, Stadium>();

        //--------- Ploeg ---------------
        CreateMap<Ploeg, PloegVM>();
        CreateMap<PloegVM, Ploeg>();

        //--------- Voor Swagger ---------------
        CreateMap<Match, MatchSwaggerVM>();

        //--------- Voor Tickets ---------------
        CreateMap<Stadium, StadiumTicketVM>();
        CreateMap<StadiumTicketVM, Stadium>();

        CreateMap<Ring, RingVM>();
        CreateMap<RingVM, Ring>();

        CreateMap<Section, SectionVM>();
        CreateMap<SectionVM, Section>();

        CreateMap<Zitplaat, ZitPlaatsVM>();
        CreateMap<ZitPlaatsVM, Zitplaat>();
    }
}
