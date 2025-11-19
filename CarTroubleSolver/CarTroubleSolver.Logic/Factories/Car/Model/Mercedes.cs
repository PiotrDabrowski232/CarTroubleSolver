using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;
using CarTroubleSolver.Shared.Models.Enums.Models;

namespace CarTroubleSolver.Logic.Factories.Car.Model
{
    public class Mercedes : IModel
    {
        public IList<string> Models()
        {
            return Enum.GetNames<MercedesBenzModels>().ToList();

        }

    }
}
