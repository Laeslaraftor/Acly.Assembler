namespace System
{
    public class Exception(string message, Exception? innerException)
    {
        public Exception() : this("An exception was thrown")
        {

        }
        public Exception(string message) : this(message, null)
        {
        }

        public string Message { get; } = message;
        public Exception? InnerException { get; } = innerException;
    }
}
