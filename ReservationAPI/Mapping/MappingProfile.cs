using AutoMapper;
using ReservationAPI.Model;
using ReservationAPI.Models;

namespace ReservationAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReservationRequest, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src => DateTime.Parse(src.DepartureTime)))
                .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => DateTime.Parse(src.ArrivalTime)));
        }
    }
}
