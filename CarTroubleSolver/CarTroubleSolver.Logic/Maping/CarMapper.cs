using AutoMapper;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Car;

namespace CarTroubleSolver.Logic.Maping
{
    public class CarMapper : Profile
    {
        public CarMapper()
        {
            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.ToString()))
                .ForMember(dest => dest.CarType, opt => opt.MapFrom(src => src.CarType.ToString()))
                .ForMember(dest => dest.DateOfProduction, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfProduction)));

            CreateMap<CarDto, Car>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => Enum.Parse<Brand>(src.Brand)))
                .ForMember(dest => dest.CarType, opt => opt.MapFrom(src => Enum.Parse<CarType>(src.CarType)))
                .ForMember(dest => dest.DateOfProduction, opt => opt.MapFrom(src => src.DateOfProduction.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
