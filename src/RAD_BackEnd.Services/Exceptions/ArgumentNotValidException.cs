namespace RAD_BackEnd.Services.Exceptions;

public class ArgumentNotValidException : Exception
{
    public ArgumentNotValidException() { }
    public ArgumentNotValidException(string message) : base(message) { }
    public ArgumentNotValidException(string message, Exception exception) { }

    public int StatusCode => 400;
}
