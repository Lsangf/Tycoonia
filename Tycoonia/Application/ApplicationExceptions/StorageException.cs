namespace Tycoonia.Application.ApplicationExceptions
{
    public class StorageException : Exception
    {
        public StorageException() : base("Resource not found in storage") { }
        public StorageException(string message) : base(message) { }
    }
}
