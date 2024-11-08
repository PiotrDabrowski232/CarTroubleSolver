using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Dto.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Message
{
    public class SendMessageCommand(SendMessageDto message, string messageId) : IRequest<bool>
    {
        public SendMessageDto Message { get; set; } = message;
        public string MessageId { get; set; } = messageId;
    }
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public SendMessageCommandHandler(CarTroubleSolverDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var guidMessage = Guid.Parse(request.MessageId);

            var message = await _dbContext.Messages.Where(x => x.Id == guidMessage).FirstOrDefaultAsync(cancellationToken);

            var tryparse = DateTime.Parse(request.Message.Date);


            Shared.Models.ExtraModels.Message sendMessage = new Shared.Models.ExtraModels.Message
            {
                Id = Guid.NewGuid(),
                Content = request.Message.Description,
                SentAt = DateTime.Now,
                DateOfVisit = DateTime.Parse(request.Message.Date),
                SenderWorkshopId = message.ReceiverWorkshopId,
                ReceiverUserId = message.SenderUserId,
                Service = message.Service,
                CarId = message.CarId,
                IsRead = false,
                PreviousMessageId = message.Id,
                Responsed = false,
                RateMessage = false
            };

            _dbContext.Messages.Add(sendMessage);

            message.Responsed = true;
            
            _dbContext.SaveChanges();

            return true;
        }
    }
}
