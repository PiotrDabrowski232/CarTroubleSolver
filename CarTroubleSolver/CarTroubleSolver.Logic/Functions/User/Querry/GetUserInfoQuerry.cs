using AutoMapper;
using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Dto.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarTroubleSolver.Logic.Functions.User.Querry
{
    public class GetUserInfoQuerry : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public GetUserInfoQuerry()
        {

        }
    }

    public class GetBasicCarConfigQueryHandler(IHttpContextAccessor httpContextAccessor, CarTroubleSolverDbContext dbContext, IMapper mapper) : IRequestHandler<GetUserInfoQuerry, UserDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly CarTroubleSolverDbContext _dbContext = dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public Task<UserDto> Handle(GetUserInfoQuerry request, CancellationToken cancellationToken)
        {
            request.Id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            var user = _dbContext.Users
                .Where(u => u.Id == request.Id)
                .Include(u => u.Cars)
                    .ThenInclude(c => c.Color)
                .FirstOrDefault();

            var userDto = _mapper.Map<UserDto>(user);

            return Task.FromResult(userDto);
        }
    }

}
