namespace PeanutTestProject.Core.Exceptions;

public class InvalidCurrencyPairException : Exception
{
    public InvalidCurrencyPairException()
    {
    }

    public InvalidCurrencyPairException(string message) : base(message)
    {
    }
}