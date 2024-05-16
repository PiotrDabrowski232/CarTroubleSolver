using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Car;
using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Query
{
    public class GetBasicCarConfigQuery : IRequest<BasicCarConfig>
    {
        public BasicCarConfig BasicCarConfig { get; set; }
        public GetBasicCarConfigQuery()
        {
        }
        public GetBasicCarConfigQuery(BasicCarConfig basicCarConfig) 
        {
            BasicCarConfig = basicCarConfig;
        }
    }

    public class GetBasicCarConfigQueryHandler : IRequestHandler<GetBasicCarConfigQuery, BasicCarConfig>
    {
        public Task<BasicCarConfig> Handle(GetBasicCarConfigQuery request, CancellationToken cancellationToken)
        {
            request.BasicCarConfig = new BasicCarConfig
            {
                Brands = Enum.GetNames<Brand>().ToList(),
                Types = Enum.GetNames<CarType>().ToList(),
            };

            return Task.FromResult(request.BasicCarConfig);
        }
    }
}
