using AutoMapper;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Workshop.Logic.Dto.Hour;

namespace CarTroubleSolver.Workshop.Logic.Maping
{
    public class HourMapper : Profile
    {
        public HourMapper()
        {
            CreateMap<WorkingHoursDto, HourConfiguration>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => Enum.Parse<DayOfWeek>(src.DayOfWeek)))
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => TimeOnly.Parse(src.From)))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => TimeOnly.Parse(src.To)))
                .ForMember(dest => dest.WorkshopId, opt => opt.MapFrom(src => Guid.Parse(src.WorkshopId)))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<HourConfiguration, WorkingHoursDto>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.ToString("HH:mm")))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.To.ToString("HH:mm")))
                .ForMember(dest => dest.WorkshopId, opt => opt.MapFrom(src => src.WorkshopId.ToString()));
        }
    }
}
