using CarTroubleSolver.Shared.Models.ExtraModels;

namespace CarTroubleSolver.Logic.Dto.Workshop
{
    public class WorkshopDetailsDto
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public long NIP { get; set; }
        public StreetDto Location { get; set; }
        
        public List<WorkshopServicesDto> Services { get; set; }
        public List<RateDetailsDto> RateDetails { get; set; }
    }
}
