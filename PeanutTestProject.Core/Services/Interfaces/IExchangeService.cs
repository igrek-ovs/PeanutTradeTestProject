using PeanutTestProject.Core.DTO_s;

namespace PeanutTestProject.Core.Services.Interfaces;

public interface IExchangeService
{
    Task<EstimateResultDto> EstimateAsync(EstimateRequestDto estimateRequestDto);
    Task<List<ExchangeRateResultDto>> GetRatesAsync(string baseCurrency, string quoteCurrency);
}