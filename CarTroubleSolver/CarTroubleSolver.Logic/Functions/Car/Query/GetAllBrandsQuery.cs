using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Query
{
    public class GetAllBrandsQuery : IRequest<List<string>>
    {
    }
}
