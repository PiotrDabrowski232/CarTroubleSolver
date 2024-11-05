using CarTroubleSolver.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Message
{
    public class SetReadCommand(Guid messageId) : IRequest<bool>
    {
        public Guid MessageId { get; set; } = messageId;
    }

    public class SetReadCommandHandler : IRequestHandler<SetReadCommand, bool>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public SetReadCommandHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(SetReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.Id == request.MessageId, cancellationToken);

            message.IsRead = true;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
