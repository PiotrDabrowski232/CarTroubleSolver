using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;
using CarTroubleSolver.Shared.Models.Enums.Models;

namespace CarTroubleSolver.Logic.Factories.Car.Model
{
    internal class KIA : IModel
    {
        public IList<string> Models()
        {
            return Enum.GetNames<KIAModels>().ToList();

        }
    }
}
