using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Workshop.Logic.Dto.Workshop
{
    public class WorkshopDetailsDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public long NIP { get; set; }
        public StreetDto Adress { get; set; }
    }
}
