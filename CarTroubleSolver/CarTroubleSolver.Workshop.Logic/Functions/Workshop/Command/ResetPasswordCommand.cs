using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Exceptions;
using CarTroubleSolver.Shared.Services.Interface;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace CarTroubleSolver.Workshop.Logic.Functions.Workshop.Command
{
    public class ResetPasswordCommand : IRequest<Guid>
    {
        public NewPasswordDto NewPassword { get; set; }
        public ResetPasswordCommand(NewPasswordDto newPassword)
        {
            NewPassword = newPassword;
        }
    }

    public class ResetPasswordCommandHandler(CarTroubleSolverDbContext context, IHttpContextAccessor client,
        IHashingService hashingService) : IRequestHandler<ResetPasswordCommand, Guid>
    {
        private readonly CarTroubleSolverDbContext _context = context;
        private readonly IHttpContextAccessor _client = client;
        private readonly IHashingService _hashingService = hashingService;
        public Task<Guid> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var id = _client.HttpContext?.User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);

                if (id == null || !Guid.TryParse(id, out Guid workshopId))
                {
                    throw new NotAuthorizedException("Authorize first");
                }

                using (var context = _context)
                {
                    var workshop = context.Workshops
                        .Where(x => x.Id == workshopId)
                        .FirstOrDefault();

                    if (!_hashingService.VerifyHashedPassword(null, workshop.Password, request.NewPassword.CurrentPassword))
                        throw new InvalidProvidedDataException("Incorrect Password");

                    workshop.Password = _hashingService.HashPassword(null, request.NewPassword.NewPassword);

                    context.Update(workshop);

                    context.SaveChangesAsync();
                }


                return Task.FromResult(workshopId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _context.Dispose();
            }
        }
    }
}
