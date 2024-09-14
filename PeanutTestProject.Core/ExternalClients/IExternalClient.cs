namespace PeanutTestProject.Core.ExternalClients;

public interface IExternalClient
{
    Task<decimal> GetExchangeRateAsync(string baseCurrency, string quoteCurrency);
}