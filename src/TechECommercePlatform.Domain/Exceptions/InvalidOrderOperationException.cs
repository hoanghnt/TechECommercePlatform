namespace TechECommercePlatform.Domain.Exceptions;

public class InvalidOrderOperationException : DomainException
{
    public InvalidOrderOperationException(string message) : base(message)
    {
    }

    public InvalidOrderOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}