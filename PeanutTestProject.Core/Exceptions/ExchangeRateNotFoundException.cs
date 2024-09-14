namespace PeanutTestProject.Core.Exceptions;

public class ExchangeRateNotFoundException : Exception
{
    public ExchangeRateNotFoundException(string pair) 
        : base($"Exchange rate for pair {pair} not found.") { }
}