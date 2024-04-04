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
            opts => opts.MapFrom(src => src.PloegUit.Naam));
        CreateMap<MatchVM, Match>();

        //--------- Stadium ---------------
        CreateMap<Stadium, StadiumVM>();
        CreateMap<StadiumVM, Stadium>();

        //--------- Ploeg ---------------
        CreateMap<Ploeg, PloegVM>();
        CreateMap<PloegVM, Ploeg>();
        }
    }
