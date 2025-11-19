using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Dto.Message;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarTroubleSolver.Workshop.Logic.Functions.Message
{
    public class ReceiveMessageQuery() : IRequest<List<ReceiveMessageBasicInfoDto>>;

    public class ReceiveMessageQueryHandler : IRequestHandler<ReceiveMessageQuery, List<ReceiveMessageBasicInfoDto>>
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly CarTroubleSolverDbContext _DbContext;

        public ReceiveMessageQueryHandler(IHttpContextAccessor httpContextAccessor, CarTroubleSolverDbContext dbContext)
        {
            _contextAccessor = httpContextAccessor;
            _DbContext = dbContext;
        }

        public async Task<List<ReceiveMessageBasicInfoDto>> Handle(ReceiveMessageQuery request, CancellationToken cancellationToken)
        {
            var workshopId = (Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return await _DbContext.Messages.Where(x => x.ReceiverWorkshopId == workshopId && !x.Responsed)
                .Select(x => new ReceiveMessageBasicInfoDto
                {
                    Id = x.Id,
                    UserName = x.SenderUser.Name,
                    Telephone = x.SenderUser.PhoneNumber,
                    Service = Enum.GetName(x.Service),
                    SendDay = x.SentAt,
                    IsRead = x.IsRead,
                    Car = new Dto.BasicCarInfoDto
                    {
                        Id = x.Car.Id,
                        Brand = Enum.GetName(x.Car.Brand),
                        Model = x.Car.Model
                    }
                })
                .OrderByDescending(x => x.IsRead)
                .ThenByDescending(x => x.SendDay)
                .ToListAsync(cancellationToken);
        }
    }
}
