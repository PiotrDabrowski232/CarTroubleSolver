using CarTroubleSolver.Logic.Dto.Message;
using CarTroubleSolver.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Logic.Functions.Message
{
    public class ReceiveMessageDetailsQuery(string id) : IRequest<MessageDetailsDto>
    {
        public string Id { get; set; } = id;
    }

    public class ReceiveMessageDetailsQueryHandler : IRequestHandler<ReceiveMessageDetailsQuery, MessageDetailsDto>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public ReceiveMessageDetailsQueryHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MessageDetailsDto> Handle(ReceiveMessageDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Messages.Where(x => x.Id == Guid.Parse(request.Id)).
                Select(x => new MessageDetailsDto
                {
                    Id = x.Id,
                    Message = x.Content,
                    WorkshopName = x.SenderWorkshop.Name,
                    IsRead = x.IsRead,
                    SentAt = x.SentAt,
                    DateOfVisit = x.DateOfVisit,
                    Service = Enum.GetName(x.Service),
                    RateMessage = x.RateMessage,
                    Car = new Dto.Car.CarBasicInfoMessageDto
                    {
                        VIN = x.Car.VIN.ToUpper(),
                        Brand = Enum.GetName(x.Car.Brand),
                        Model = x.Car.Model,
                        Engine = x.Car.Engine,
                        Mileage = x.Car.Mileage,
                    }

                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
