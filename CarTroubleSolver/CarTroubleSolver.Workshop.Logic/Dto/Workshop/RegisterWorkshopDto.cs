using CarTroubleSolver.Workshop.Logic.Dto.Street;

namespace CarTroubleSolver.Workshop.Logic.Dto.Workshop
{
    public class RegisterWorkshopDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public long NIP { get; set; }
        public StreetDto Street { get; set; }
    }
}
