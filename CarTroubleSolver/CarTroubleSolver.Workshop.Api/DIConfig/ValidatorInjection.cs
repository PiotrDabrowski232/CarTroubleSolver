namespace CarTroubleSolver.Workshop.Api.DIConfig
{
    public static class ValidatorInjection
    {
        public static IServiceCollection WithValidation(this IServiceCollection Services)
        {
            return Services;
        }
    }
}
