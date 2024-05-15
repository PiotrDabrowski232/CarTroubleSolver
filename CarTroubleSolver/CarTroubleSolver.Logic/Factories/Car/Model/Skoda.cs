using CarTroubleSolver.Data.Models.Enums.Models;
using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Factories.Car.Model
{
    internal class Skoda : IModel
    {
        public IList<string> Models()
        {            
            return Enum.GetNames<SkodaModels>().ToList();
        }
    
    }
}
