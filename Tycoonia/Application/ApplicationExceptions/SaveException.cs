namespace Tycoonia.Application.ApplicationExceptions
{
    public class SaveException : Exception
    {
        public SaveException() : base("Save error") { }
        public SaveException(string message) : base(message) { }
    }
}
