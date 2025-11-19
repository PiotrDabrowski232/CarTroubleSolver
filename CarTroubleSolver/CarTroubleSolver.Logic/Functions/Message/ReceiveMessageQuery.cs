using CarTroubleSolver.Logic.Dto.Message;
using CarTroubleSolver.Shared.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarTroubleSolver.Logic.Functions.Message
{
    public class ReceiveMessageQuery : IRequest<List<ReceivedMessageDto>>;

    public class ReceiveMessageQueryHandler : IRequestHandler<ReceiveMessageQuery, List<ReceivedMessageDto>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CarTroubleSolverDbContext _dbContext;

        public ReceiveMessageQueryHandler(IHttpContextAccessor httpContextAccessor, CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ReceivedMessageDto>> Handle(ReceiveMessageQuery request, CancellationToken cancellationToken)
        {
            var userId = (Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return await _dbContext.Messages.Where(x => x.ReceiverUserId == userId && !x.Responsed)
                .Select(x => new ReceivedMessageDto
                {
                    Id = x.Id,
                    Message = x.Content,
                    WorkshopName = x.SenderWorkshop.Name,
                    IsRead = x.IsRead,
                    SentAt = x.SentAt,
                    DateOfVisit = x.DateOfVisit,
                    Brand = Enum.GetName(x.Car.Brand),
                    Model = x.Car.Model,
                    Service = Enum.GetName(x.Service)
                }).ToListAsync(cancellationToken);

        }
    }
}
