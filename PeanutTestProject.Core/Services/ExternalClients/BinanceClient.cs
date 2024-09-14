using Binance.Net.Clients;
using PeanutTestProject.Core.Exceptions;
using PeanutTestProject.Core.Services.Interfaces;

namespace PeanutTestProject.Core.Services.ExternalClients;

public class BinanceClient : IExternalClient
{
    private readonly BinanceRestClient _binanceRestClient;

    public BinanceClient(BinanceRestClient binanceRestClient)
    {
        _binanceRestClient = binanceRestClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string baseCurrency, string quoteCurrency)
    {
        var pair = $"{baseCurrency}{quoteCurrency}";
        var reversePair = $"{quoteCurrency}{baseCurrency}";

        var result = await _binanceRestClient.SpotApi.ExchangeData.GetPriceAsync(pair);

        if (result?.Data?.Price == null)
        {
            result = await _binanceRestClient.SpotApi.ExchangeData.GetPriceAsync(reversePair);

            if (result?.Data?.Price != null) return 1 / result.Data.Price;

            throw new ExchangeRateNotFoundException(pair);
        }

        return result.Data.Price;
    }
}