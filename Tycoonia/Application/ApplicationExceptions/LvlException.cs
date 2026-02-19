namespace Tycoonia.Application.ApplicationExceptions
{
    public class LvlException : Exception
    {
        public LvlException() : base("The maximum level has been reached") { }
        public LvlException(string message) : base(message) {}
    }
}
