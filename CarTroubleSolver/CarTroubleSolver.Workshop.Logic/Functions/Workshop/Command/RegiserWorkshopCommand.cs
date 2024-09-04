using AutoMapper;
using CarTroubleSolver.Shared.Exceptions;
using CarTroubleSolver.Workshop.Data.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Workshop.Command
{
    public class RegiserWorkshopCommand : IRequest<Guid>
    {
        public RegisterWorkshopDto Workshop { get; set; }

        public RegiserWorkshopCommand(RegisterWorkshopDto workshop)
        {
            Workshop = workshop;
        }
    }
    public class AddCarCommandHandler(IWorkshopRepository workshopRepository, IMapper mapper) : IRequestHandler<RegiserWorkshopCommand, Guid>
    {
        private readonly IWorkshopRepository _workshopRepository = workshopRepository;
        private readonly IMapper _mapper = mapper;

        public Task<Guid> Handle(RegiserWorkshopCommand request, CancellationToken cancellationToken)
        {
            if (_workshopRepository.WorkshopExist(request.Workshop.NIP))
                throw new EntityExistException();

            var workshop = _mapper.Map<Data.Models.Workshop>(request.Workshop);

           var result = _workshopRepository.Add(workshop);

            return Task.FromResult(result);
        }
    }
}
