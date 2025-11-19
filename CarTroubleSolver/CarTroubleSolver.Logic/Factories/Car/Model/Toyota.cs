using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;
using CarTroubleSolver.Shared.Models.Enums.Models;

namespace CarTroubleSolver.Logic.Factories.Car.Model
{
    internal class Toyota : IModel
    {
        public IList<string> Models()
        {
            return Enum.GetNames<ToyotaModels>().ToList();

        }

    }
}
