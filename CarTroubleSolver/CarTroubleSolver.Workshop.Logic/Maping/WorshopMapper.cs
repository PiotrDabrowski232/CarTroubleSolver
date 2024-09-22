using AutoMapper;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;


namespace CarTroubleSolver.Workshop.Logic.Maping
{
    internal class WorshopMapper : Profile
    {
        public WorshopMapper()
        {
            CreateMap<Shared.Models.WorkshopPanel.Workshop, RegisterWorkshopDto>();
            CreateMap<RegisterWorkshopDto, Shared.Models.WorkshopPanel.Workshop>();
        }
    }
}
