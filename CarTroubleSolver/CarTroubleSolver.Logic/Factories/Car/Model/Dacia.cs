using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;
using CarTroubleSolver.Shared.Models.Enums.Models;

namespace CarTroubleSolver.Logic.Factories.Car.Model
{
    internal class Dacia : IModel
    {
        public IList<string> Models()
        {
            return Enum.GetNames<DaciaModels>().ToList();

        }
    }
}
