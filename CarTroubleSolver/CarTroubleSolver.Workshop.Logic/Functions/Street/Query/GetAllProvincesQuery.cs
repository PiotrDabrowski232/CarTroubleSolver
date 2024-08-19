using CarTroubleSolver.Workshop.Data.Models.Enum;
using CarTroubleSolver.Workshop.Logic.Dto.Street;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Street.Query
{
    public class GetAllProvincesQuery : IRequest<ProvincesDto>
    {
        public ProvincesDto Provinces { get; set; }
    }

    public class GetBasicCarConfigQueryHandler : IRequestHandler<GetAllProvincesQuery, ProvincesDto>
    {
        public Task<ProvincesDto> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
        {
            request.Provinces = new ProvincesDto
            {
                Province = Enum.GetNames<Province>().ToList()
            };

            return Task.FromResult(request.Provinces);
        }
    }
}