namespace PeanutTestProject.Core.Exceptions;

public class InvalidCurrencyPairException : Exception
{
    public InvalidCurrencyPairException() : base() { }
    
    public InvalidCurrencyPairException(string message) : base(message) { }
}