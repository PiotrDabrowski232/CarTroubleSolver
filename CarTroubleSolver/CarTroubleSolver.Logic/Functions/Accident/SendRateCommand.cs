using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO;

namespace CarTroubleSolver.Logic.Functions.Accident
{
    public class SendRateCommand(string messageId, RateDto rate) : IRequest<bool>
    {
        public string MessageId { get; set; } = messageId;
        public RateDto Rate { get; set; } = rate;
    }

    public class SendRateCommandHandler : IRequestHandler<SendRateCommand, bool>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public SendRateCommandHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(SendRateCommand request, CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.MessageId), cancellationToken);

            var rate = new Rating
            {
                Id = Guid.NewGuid(),
                Comment = request.Rate.Comment,
                Rate = request.Rate.Rate,
                UserId = (Guid)message.ReceiverUserId,
                WorkshopId = (Guid)message.SenderWorkshopId,
                Date = DateTime.Now,
            };

            message.Responsed = true;

            _dbContext.Rating.Add(rate);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
