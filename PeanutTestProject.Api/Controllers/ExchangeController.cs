using Microsoft.AspNetCore.Mvc;
using PeanutTestProject.Core.DTO_s;
using PeanutTestProject.Core.Services.Interfaces;

namespace PeanutTestProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeService _exchangeService;

    public ExchangeController(IExchangeService exchangeService)
    {
        _exchangeService = exchangeService;
    }

    [HttpGet("estimate")]
    public async Task<ActionResult<EstimateResultDto>> Estimate([FromQuery] EstimateRequestDto request)
    {
        var result = await _exchangeService.EstimateAsync(request);
        return Ok(result);
    }

    [HttpGet("getRates")]
    public async Task<ActionResult<List<ExchangeRateResultDto>>> GetRates(string baseCurrency, string quoteCurrency)
    {
        var result = await _exchangeService.GetRatesAsync(baseCurrency, quoteCurrency);
        return Ok(result);
    }
}