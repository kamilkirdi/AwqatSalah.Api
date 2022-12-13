using Microsoft.AspNetCore.Mvc;

namespace DiyanetNamazVakti.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DailyContentController : ControllerBase
{
    private readonly IDailyContentService _dailyContentService;

    public DailyContentController(IDailyContentService dailyContentService)
    {
        _dailyContentService = dailyContentService;
    }

    [HttpGet]
    public async Task<ActionResult<IResult>> Get()
    {
        return new SuccessDataResult<DailyContentModel>(await _dailyContentService.DailyContent());
    }

}
