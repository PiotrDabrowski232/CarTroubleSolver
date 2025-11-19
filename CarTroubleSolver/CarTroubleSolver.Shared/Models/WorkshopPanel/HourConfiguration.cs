namespace CarTroubleSolver.Shared.Models.WorkshopPanel;

public class HourConfiguration
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }

    public Guid WorkshopId { get; set; }
    public virtual Workshop Workshop { get; set; }
}
