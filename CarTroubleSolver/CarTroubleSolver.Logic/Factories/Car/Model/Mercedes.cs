using CarTroubleSolver.Data.Models.Enums.Models;
using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;

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
