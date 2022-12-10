using Microsoft.AspNetCore.Mvc;

namespace DiyanetNamazVakti.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[EnableRateLimiting(nameof(RateLimitConstants.DefaultRateLimit))]
public class AwqatSalahController : ControllerBase
{
    private readonly IAwqatSalahService _awqatSalahService;
    private readonly ICacheService _cacheService;

    public AwqatSalahController(IAwqatSalahService awqatSalahService, ICacheService cacheService)
    {
        _awqatSalahService = awqatSalahService;
        _cacheService = cacheService;
    }

    [HttpGet("Daily/{cityId}")]
    public async Task<ActionResult<IResult>> AwqatSalahDaily(int cityId)
    {
        return new SuccessDataResult<List<AwqatSalahModel>>(await _awqatSalahService.DailyAwqatSalah(cityId));
    }

    [HttpGet("Weekly/{cityId}")]
    public async Task<ActionResult<IResult>> AwqatSalahWeekly(int cityId)
    {
        return new SuccessDataResult<List<AwqatSalahModel>>(await _awqatSalahService.WeeklyAwqatSalah(cityId));
    }

    [HttpGet("Monthly/{cityId}")]
    public async Task<ActionResult<IResult>> AwqatSalahMonthly(int cityId)
    {
        return new SuccessDataResult<List<AwqatSalahModel>>(await _awqatSalahService.MonthlyAwqatSalah(cityId));
    }

    [HttpGet("Eid/{cityId}")]
    public async Task<ActionResult<IResult>> AwqatSalahEid(int cityId)
    {
        return new SuccessDataResult<EidAwqatSalahModel>(await _awqatSalahService.EidAwqatSalah(cityId));
    }

    [HttpGet("Ramadan/{cityId}")]
    public async Task<ActionResult<IResult>> AwqatSalahRamadan(int cityId)
    {
        return new SuccessDataResult<List<AwqatSalahModel>>(await _awqatSalahService.RamadanAwqatSalah(cityId));
    }

}
