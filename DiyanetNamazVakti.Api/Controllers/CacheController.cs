using Microsoft.AspNetCore.Mvc;

namespace DiyanetNamazVakti.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CacheController : ControllerBase
{
    private readonly ICacheService _cacheService;

    public CacheController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("ClearCache")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult<IResult> ClearCache()
    {
        _cacheService.Clear();
        return new SuccessDataResult<bool>(true);
    }

    [HttpGet("GetCacheNames")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult<IResult> GetCacheName()
    {
        return new SuccessDataResult<List<string>>(_cacheService.GetAllKeys());
    }
}
