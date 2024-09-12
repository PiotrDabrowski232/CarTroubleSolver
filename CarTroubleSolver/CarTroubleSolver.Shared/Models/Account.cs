namespace CarTroubleSolver.Shared.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
    }
}
