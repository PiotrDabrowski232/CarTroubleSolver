using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;
using CarTroubleSolver.Shared.Models.Enums.Models;

namespace CarTroubleSolver.Logic.Factories.Car.Model
{
    internal class Hyundai : IModel
    {
        public IList<string> Models()
        {
            return Enum.GetNames<HyundaiModels>().ToList();

        }
    }
}
