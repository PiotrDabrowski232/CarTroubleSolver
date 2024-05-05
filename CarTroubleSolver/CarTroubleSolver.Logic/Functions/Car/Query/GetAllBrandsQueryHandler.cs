using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Query
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, List<string>>
    {
        public Task<List<string>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<string>());
        }
    }
}
