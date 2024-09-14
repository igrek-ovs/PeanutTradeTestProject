namespace PeanutTestProject.Core.Exceptions;

public class InvalidInputAmountException : Exception
{
    public InvalidInputAmountException(string message) : base(message)
    {
    }
}