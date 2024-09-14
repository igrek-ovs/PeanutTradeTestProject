using PeanutTestProject.Core.DTO_s;
using PeanutTestProject.Core.Exceptions;
using PeanutTestProject.Core.Services.Interfaces;

namespace PeanutTestProject.Core.Services;

public class ExchangeService : IExchangeService
{
    private readonly IEnumerable<IExternalClient> _externalClients;

    public ExchangeService(IEnumerable<IExternalClient> externalClients)
    {
        _externalClients = externalClients;
    }

    public async Task<EstimateResultDto> EstimateAsync(EstimateRequestDto estimateRequestDto)
    {
        ValidateCurrencyPair(estimateRequestDto.InputCurrency, estimateRequestDto.OutputCurrency);

        if (estimateRequestDto.InputAmount <= 0)
            throw new InvalidInputAmountException("Input amount must be greater than 0");

        decimal bestOutput = 0;
        var bestExchange = string.Empty;

        foreach (var client in _externalClients)
        {
            var rate = await client.GetExchangeRateAsync(estimateRequestDto.InputCurrency,
                estimateRequestDto.OutputCurrency);
            var output = estimateRequestDto.InputAmount * rate;

            if (output > bestOutput)
            {
                bestOutput = output;
                bestExchange = client.GetType().Name.Replace("Client", "").ToLower();
            }
        }

        return new EstimateResultDto(bestExchange, bestOutput);
    }

    public async Task<List<ExchangeRateResultDto>> GetRatesAsync(string baseCurrency, string quoteCurrency)
    {
        ValidateCurrencyPair(baseCurrency, quoteCurrency);

        var rates = new List<ExchangeRateResultDto>();

        foreach (var client in _externalClients)
        {
            var rate = await client.GetExchangeRateAsync(baseCurrency, quoteCurrency);
            var exchangeName = client.GetType().Name.Replace("Client", "").ToLower();
            rates.Add(new ExchangeRateResultDto(exchangeName, rate));
        }

        return rates;
    }

    private void ValidateCurrencyPair(string baseCurrency, string quoteCurrency)
    {
        if (baseCurrency == quoteCurrency) throw new InvalidCurrencyPairException("Same currencies are not allowed!");
    }
}