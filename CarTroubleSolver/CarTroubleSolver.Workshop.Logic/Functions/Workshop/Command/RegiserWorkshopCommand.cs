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
    public class AddCarCommandHandler : IRequestHandler<RegiserWorkshopCommand, RegisterWorkshopDto>
    {
        public Task<RegisterWorkshopDto> Handle(RegiserWorkshopCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
