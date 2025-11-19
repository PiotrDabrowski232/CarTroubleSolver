using CarTroubleSolver.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Logic.Functions.Message
{
    public class SetUserMessageReadCommand(string messageId) : IRequest<bool>
    {
        public string MessageId { get; set; } = messageId;
    }

    public class SetUserMessageReadCommandHandler : IRequestHandler<SetUserMessageReadCommand, bool>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public SetUserMessageReadCommandHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(SetUserMessageReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.MessageId), cancellationToken);

            message.IsRead = true;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
