namespace CarTroubleSolver.Shared.Services.Interface
{
    public interface ITokenService
    {
        public string GenerateJwt(string id, string name, string email);
    }
}
