namespace Tycoonia.Application.ApplicationExceptions
{
    public class InputException : Exception
    {
        public InputException() : base("Input incorrect") { }
        public InputException(string message) : base(message) { }
    }
}
