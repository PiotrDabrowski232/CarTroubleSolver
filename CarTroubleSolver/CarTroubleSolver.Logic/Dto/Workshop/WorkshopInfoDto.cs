using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Dto.Workshop
{
    public class WorkshopInfoDto
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Services { get; set; }
        public double Rating { get; set; }
        public string City { get; set; }
    }
}
