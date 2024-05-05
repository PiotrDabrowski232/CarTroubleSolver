using CarTroubleSolver.Data.Models.Enums;
using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Query
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, List<string>>
    {
        public Task<List<string>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var Brands = Enum.GetNames<Brand>().ToList();
            return Task.FromResult(Brands);
        }
    }
}
