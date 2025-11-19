using CarTroubleSolver.Shared.Models.Enum;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Services
{
    public class GetServiceTypesQuery : IRequest<IEnumerable<string>>;

    public class GetServiceTypesQueryHandler : IRequestHandler<GetServiceTypesQuery, IEnumerable<string>>
    {
        public Task<IEnumerable<string>> Handle(GetServiceTypesQuery request, CancellationToken cancellationToken)
        {
            var servicesName = Enum.GetNames(typeof(ServiceType)).AsEnumerable();

            return Task.FromResult(servicesName);
        }
    }
}
