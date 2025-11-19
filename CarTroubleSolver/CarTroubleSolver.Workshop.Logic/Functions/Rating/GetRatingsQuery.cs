using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Dto.Rating;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarTroubleSolver.Workshop.Logic.Functions.Rating
{
    public class GetRatingsQuery : IRequest<RatingDto>;

    public class GetRatingsQueryHandler : IRequestHandler<GetRatingsQuery, RatingDto>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public GetRatingsQueryHandler(CarTroubleSolverDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<RatingDto> Handle(GetRatingsQuery request, CancellationToken cancellationToken)
        {
            var workshopId = (Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return await _dbContext.Workshops
                .Where(x => x.Id == workshopId)
                .Select(x => new RatingDto
                {
                    Average = x.Ratings.Average(x => x.Rate),
                    WorkshopName = x.Name,
                    RatingItems = x.Ratings
                    .Select(x => new RatingItemsDto
                    {
                        Content = x.Comment,
                        Rate = x.Rate,
                        Date = x.Date,
                        UserName = $"{x.User.Name} {x.User.Surname}"
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
