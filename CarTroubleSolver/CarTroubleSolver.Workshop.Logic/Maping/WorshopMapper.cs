using AutoMapper;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;

namespace CarTroubleSolver.Workshop.Logic.Maping
{
    internal class WorshopMapper : Profile
    {
        public WorshopMapper()
        {
            CreateMap<Data.Models.Workshop, RegisterWorkshopDto>();
            CreateMap<RegisterWorkshopDto, Data.Models.Workshop>();
        }
    }
}
