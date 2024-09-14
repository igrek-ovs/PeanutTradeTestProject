using System.Text.Json;
using Kucoin.Net.Clients;
using PeanutTestProject.Core.Exceptions;

namespace PeanutTestProject.Core.ExternalClients;

public class KuCoinClient : IExternalClient
{
    private readonly KucoinRestClient _kucoinRestClient; 

    public KuCoinClient(KucoinRestClient kucoinRestClient)
    {
        _kucoinRestClient = kucoinRestClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string baseCurrency, string quoteCurrency)
    {
        var pair = $"{baseCurrency}-{quoteCurrency}";
        var reversePair = $"{quoteCurrency}-{baseCurrency}";

        var result = await _kucoinRestClient.SpotApi.ExchangeData.GetTickerAsync(pair);
        
        if (result?.Data?.LastPrice == null)
        {
            result = await _kucoinRestClient.SpotApi.ExchangeData.GetTickerAsync(reversePair);
            
            if (result?.Data?.LastPrice != null)
            {
                return 1 / result.Data.LastPrice.Value;
            }

            throw new ExchangeRateNotFoundException(pair);
        }

        return result.Data.LastPrice.Value;
    }
}
