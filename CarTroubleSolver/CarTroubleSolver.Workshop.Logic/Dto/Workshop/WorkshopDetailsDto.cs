using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Workshop.Logic.Dto.Hour;

namespace CarTroubleSolver.Workshop.Logic.Dto.Workshop
{
    public class WorkshopDetailsDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public long NIP { get; set; }
        public StreetDto Adress { get; set; }
        public ICollection<HoursDto>? Hours { get; set; }
    }
}
