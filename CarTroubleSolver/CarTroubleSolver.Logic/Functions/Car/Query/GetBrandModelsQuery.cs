using CarTroubleSolver.Logic.Factories.Car.Model.ModelFactory;
using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Query
{
    public class GetBrandModelsQuery : IRequest<List<string>>
    {
        public string Brand { get; set; }

        public GetBrandModelsQuery(string brand)
        {
            Brand = brand;
        }
    }

    public class GetBrandModelsQueryHandler : IRequestHandler<GetBrandModelsQuery, List<string>>
    {
        private readonly ModelFactory _modelFactory;

        public GetBrandModelsQueryHandler(ModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
        }
        public Task<List<string>> Handle(GetBrandModelsQuery request, CancellationToken cancellationToken)
        {
            var models = _modelFactory.GetModel(request.Brand).Models().ToList();
            return Task.FromResult(models);
        }
    }
}
