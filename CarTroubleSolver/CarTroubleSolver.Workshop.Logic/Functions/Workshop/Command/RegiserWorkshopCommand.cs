using CarTroubleSolver.Workshop.Data.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Workshop.Command
{
    public class RegiserWorkshopCommand : IRequest<RegisterWorkshopDto>
    {
        public RegisterWorkshopDto Workshop { get; set; }
         
        public RegiserWorkshopCommand(RegisterWorkshopDto workshop)
        {
            Workshop = workshop;
        }
    }
    public class AddCarCommandHandler(IWorkshopRepository workshopRepository) : IRequestHandler<RegiserWorkshopCommand, RegisterWorkshopDto>
    {
        private readonly IWorkshopRepository _workshopRepository = workshopRepository;
        public Task<RegisterWorkshopDto> Handle(RegiserWorkshopCommand request, CancellationToken cancellationToken)
        {
            if(_workshopRepository.WorkshopExist(Workshop))
            throw new NotImplementedException();
        }
    }
}
