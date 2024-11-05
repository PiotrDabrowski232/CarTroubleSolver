using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.ExtraModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CarTroubleSolver.Logic.Functions.Message
{
    public class SendMessageCommand(MessageDto message) : IRequest<bool>
    {
        public MessageDto Message { get; set; } = message;
    }

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly CarTroubleSolverDbContext _dbContext;
        public SendMessageCommandHandler(IHttpContextAccessor httpContextAccessor, CarTroubleSolverDbContext dbContext)
        {
            _contextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var userId = (Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            Shared.Models.ExtraModels.Message message = new Shared.Models.ExtraModels.Message
            {
                Id = Guid.NewGuid(),
                Content = request.Message.Description,
                SentAt = DateTime.Now,
                SenderUserId = userId,
                ReceiverWorkshopId = request.Message.WorkshopId,
                Service = (ServiceType)Enum.Parse(typeof(ServiceType), request.Message.Service),
                CarId = request.Message.CarId, 
            };

            _dbContext.Messages.Add(message);

            _dbContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
