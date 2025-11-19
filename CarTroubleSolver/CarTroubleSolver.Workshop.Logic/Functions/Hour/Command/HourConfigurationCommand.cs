using AutoMapper;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Hour;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Hour.Command
{
    public class HourConfigurationCommand : IRequest<ICollection<HoursDto>>
    {
        public IList<WorkingHoursDto> Hours { get; set; }

        public HourConfigurationCommand(IList<WorkingHoursDto> hours)
        {
            Hours = hours;
        }
    }

    public class HourConfigurationCommandHandler : IRequestHandler<HourConfigurationCommand, ICollection<HoursDto>>
    {
        private readonly IHourRepository _hourRepository;
        private readonly IMapper _mapper;

        public HourConfigurationCommandHandler(IHourRepository hourRepository, IMapper mapper)
        {
            _hourRepository = hourRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<HoursDto>> Handle(HourConfigurationCommand request, CancellationToken cancellationToken)
        {
            var workshopId = Guid.Parse(request.Hours.First().WorkshopId);
            var existingHours = await _hourRepository.GetAll()
                .Where(x => x.WorkshopId == workshopId)
                .ToListAsync(cancellationToken);

            var requestHours = _mapper.Map<IList<HourConfiguration>>(request.Hours);

            if (!existingHours.Any())
            {
                await _hourRepository.AddRange(requestHours);
            }
            else
            {
                var updates = requestHours.Where(rh => existingHours.Any(eh => eh.DayOfWeek == rh.DayOfWeek))
                    .ToList();

                foreach (var update in updates)
                {
                    var existingHour = existingHours.First(eh => eh.DayOfWeek == update.DayOfWeek);
                    if (HasChanges(existingHour, update))
                    {
                        existingHour.From = update.From;
                        existingHour.To = update.To;
                        await _hourRepository.Update(existingHour);
                    }
                }
            }

            await _hourRepository.SaveChanges();

            return _mapper.Map<ICollection<HoursDto>>(request.Hours);
        }

        private bool HasChanges(HourConfiguration savedTime, HourConfiguration futureTime)
        {
            return savedTime.From != futureTime.From || savedTime.To != futureTime.To;
        }
    }
}
