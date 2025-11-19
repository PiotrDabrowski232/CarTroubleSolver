using CarTroubleSolver.Logic.Factories.Car.Model.Interfaces;

namespace CarTroubleSolver.Logic.Factories.Car.Model.ModelFactory
{
    public class ModelFactory
    {
        public IModel GetModel(string Brand)
        {
            switch (Brand)
            {
                case "Skoda":
                    return new Skoda();
                case "KIA":
                    return new KIA();
                case "Volkswagen":
                    return new Volkswagen();
                case "Hyundai":
                    return new Hyundai();
                case "BMW":
                    return new BMW();
                case "MercedesBenz":
                    return new Mercedes();
                case "Dacia":
                    return new Dacia();
                case "Audi":
                    return new Audi();
                case "Ford":
                    return new Ford();
                default:
                    throw new ArgumentException($"Nieznany typ samochodu");
            }
        }
    }
}
