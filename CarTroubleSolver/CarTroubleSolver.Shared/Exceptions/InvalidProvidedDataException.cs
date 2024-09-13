namespace CarTroubleSolver.Shared.Exceptions
{
    public class InvalidProvidedDataException : Exception
    {
        public InvalidProvidedDataException() { }
        public InvalidProvidedDataException(string message) : base(message) { }
    }
}
