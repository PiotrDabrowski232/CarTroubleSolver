using CarTroubleSolver.Shared.Models.ExtraModels;

namespace CarTroubleSolver.Shared.Models.WorkshopPanel
{
    public class Workshop : Account
    {
        public long NIP { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public virtual ICollection<HourConfiguration>? OpenHours { get; set; }
    }
}
