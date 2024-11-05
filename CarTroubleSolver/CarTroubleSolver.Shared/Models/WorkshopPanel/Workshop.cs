using CarTroubleSolver.Shared.Models.ExtraModels;
using NetTopologySuite.Geometries;

namespace CarTroubleSolver.Shared.Models.WorkshopPanel
{
    public class Workshop : Account
    {
        public long NIP { get; set; }
        public Point Location { get; set; }

        public virtual ICollection<HourConfiguration>? OpenHours { get; set; }
        public virtual ICollection<WorkshopServices>? Services { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Accident> Accidents { get; set; }
    }
}
