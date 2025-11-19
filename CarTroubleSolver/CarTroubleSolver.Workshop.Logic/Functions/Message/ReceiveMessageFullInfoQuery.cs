using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Dto.Message;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarTroubleSolver.Workshop.Logic.Functions.Message
{
    public class ReceiveMessageFullInfoQuery(Guid messageId) : IRequest<ReceiveMessageDto>
    {
        public Guid MessageId { get; set; } = messageId;

        public class ReceiveMessageFullInfoQueryHandler : IRequestHandler<ReceiveMessageFullInfoQuery, ReceiveMessageDto>
        {

            private readonly CarTroubleSolverDbContext _DbContext;

            public ReceiveMessageFullInfoQueryHandler(CarTroubleSolverDbContext dbContext)
            {
                _DbContext = dbContext;
            }

            public async Task<ReceiveMessageDto> Handle(ReceiveMessageFullInfoQuery request, CancellationToken cancellationToken)
            {

                return await _DbContext.Messages.Where(x => x.Id == request.MessageId)
                    .Select(x => new ReceiveMessageDto
                    {
                        Description = x.Content,
                        UserId = (Guid)x.SenderUserId,
                        UserName = x.SenderUser.Name,
                        Telephone = x.SenderUser.PhoneNumber,
                        Service = Enum.GetName(x.Service),
                        WorkshopId = (Guid)x.ReceiverWorkshopId,
                        SendDay = x.SentAt,
                        IsRead = x.IsRead,
                        Car = new Dto.CarDto
                        {
                            Id = x.Car.Id,
                            Brand = Enum.GetName(x.Car.Brand),
                            Model = x.Car.Model,
                            Mileage = x.Car.Mileage,
                            Engine = x.Car.Engine,
                        }
                    }).FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
