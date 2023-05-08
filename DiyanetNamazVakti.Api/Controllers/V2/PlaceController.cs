using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace PrayerTime.Web.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class PlaceController : ControllerBase
{
    private readonly IPlaceService _placeService;

    public PlaceController(IPlaceService placeService)
    {
        _placeService = placeService;
    }

    [HttpGet("Countries")]
    public async Task<ActionResult<IResult>> Country()
    {
        return new SuccessDataResult<List<CountryModel>>(await _placeService.GetCountriesV2());
    }

    [HttpGet("Cities/{stateId}")]
    public async Task<ActionResult<IResult>> City(int stateId)
    {
        return new SuccessDataResult<List<CityWithParentsModel>>(await _placeService.GetCitiesByStateV2(stateId));
    }

    [HttpGet("States/{countryId}")]
    public async Task<ActionResult<IResult>> State(int countryId)
    {
        return new SuccessDataResult<List<StateWithParentModel>>(await _placeService.GetStatesByCountryV2(countryId));
    }

    [HttpGet("States")]
    public async Task<ActionResult<IResult>> GetAllStates()
    {
        return new SuccessDataResult<List<StateWithParentModel>>(await _placeService.GetStatesV2());
    }

    [HttpGet("Cities")]
    public async Task<ActionResult<IResult>> GetAllCities()
    {
        return new SuccessDataResult<List<CityWithParentsModel>>(await _placeService.GetCitiesV2());
    }
}
