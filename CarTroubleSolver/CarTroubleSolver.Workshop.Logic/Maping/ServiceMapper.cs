using AutoMapper;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Workshop.Logic.Dto.Service;

namespace CarTroubleSolver.Workshop.Logic.Maping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkshopServices, ServiceDto>()
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.Service.ToString()));

            CreateMap<ServiceDto, WorkshopServices>()
                .ForMember(dest => dest.Service, opt => opt.MapFrom(src => Enum.Parse<ServiceType>(src.ServiceType)))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
