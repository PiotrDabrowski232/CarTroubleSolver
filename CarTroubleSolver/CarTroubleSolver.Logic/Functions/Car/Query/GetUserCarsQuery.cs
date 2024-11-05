using AutoMapper;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Shared.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarTroubleSolver.Logic.Functions.Car.Query
{
    public class GetUserCarsQuery : IRequest<List<CarBasicInfoDto>>;

    public class GetUserCarsQueryHandler : IRequestHandler<GetUserCarsQuery, List<CarBasicInfoDto>>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserCarsQueryHandler(CarTroubleSolverDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<CarBasicInfoDto>> Handle(GetUserCarsQuery request, CancellationToken cancellationToken)
        {
            var userid = (Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            var cars = await _dbContext.Cars
                .Where(x => x.OwnerId == userid)
                .ToListAsync(cancellationToken);

            var mappedCars = _mapper.Map<List<CarBasicInfoDto>>(cars);

            return mappedCars;
        }
    }
}
