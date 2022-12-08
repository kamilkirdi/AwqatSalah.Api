using Microsoft.AspNetCore.Mvc;

namespace DiyanetNamazVakti.Api.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        return new SuccessDataResult<List<IdCodeName<int>>>(await _placeService.GetCountries());
    }

    [HttpGet("States")]
    public async Task<ActionResult<IResult>> GetAllStates()
    {
        return new SuccessDataResult<List<IdCodeName<int>>>(await _placeService.GetStates());
    }

    [HttpGet("States/{countryId}")]
    public async Task<ActionResult<IResult>> State(int countryId)
    {
        return new SuccessDataResult<List<IdCodeName<int>>>(await _placeService.GetStatesByCountry(countryId));
    }

    [HttpGet("Cities")]
    public async Task<ActionResult<IResult>> GetAllCities()
    {
        return new SuccessDataResult<List<IdCodeName<int>>>(await _placeService.GetCities());
    }

    [HttpGet("Cities/{stateId}")]
    public async Task<ActionResult<IResult>> City(int stateId)
    {
        return new SuccessDataResult<List<IdCodeName<int>>>(await _placeService.GetCitiesByState(stateId));
    }

    [HttpGet("CityDetail/{cityId}")]
    public async Task<ActionResult<IResult>> CityDetail(int cityId)
    {
        return new SuccessDataResult<CityDetailModel>(await _placeService.GetCity(cityId));
    }
}
