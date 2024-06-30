using AutoMapper;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Dto.Car;

namespace CarTroubleSolver.Logic.Maping
{
    public class CarMapper : Profile
    {
        public CarMapper()
        {
            CreateMap<CarColor, Color>()
            .ForMember(dest => dest.Red, opt => opt.MapFrom(src => src.Red))
            .ForMember(dest => dest.Green, opt => opt.MapFrom(src => src.Green))
            .ForMember(dest => dest.Blue, opt => opt.MapFrom(src => src.Blue));

            CreateMap<Color, CarColor>()
                .ForMember(dest => dest.Red, opt => opt.MapFrom(src => src.Red))
                .ForMember(dest => dest.Green, opt => opt.MapFrom(src => src.Green))
                .ForMember(dest => dest.Blue, opt => opt.MapFrom(src => src.Blue));

            CreateMap<Color, CarColor>();
            CreateMap<CarColor, Color>();

            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.ToString()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.CarType.ToString()))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));

            CreateMap<CarDto, Car>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => Enum.Parse<Brand>(src.Brand)))
                .ForMember(dest => dest.CarType, opt => opt.MapFrom(src => Enum.Parse<CarType>(src.Type)))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));

            CreateMap<Car, CarBasicInfoDto>();
            CreateMap<CarBasicInfoDto, Car>();

        }
    }
}
