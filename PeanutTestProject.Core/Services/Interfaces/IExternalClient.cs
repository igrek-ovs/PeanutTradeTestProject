namespace PeanutTestProject.Core.Services.Interfaces;

public interface IExternalClient
{
    Task<decimal> GetExchangeRateAsync(string baseCurrency, string quoteCurrency);
}