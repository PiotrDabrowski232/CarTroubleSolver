using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Factories.Car.Model.ModelFactory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
