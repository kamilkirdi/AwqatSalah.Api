using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace PrayerTime.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[PrayerTimeApiAuthorize]
//[EnableRateLimiting(nameof(RateLimitConstants.DefaultRateLimit))]
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

    [HttpGet("Mosques")]
    public async Task<ActionResult<IResult>> GetAllMosques()
    {
        return new SuccessDataResult<List<MosqueModel>>(await _placeService.GetMosques());
    }

    [HttpGet("MosquesByName")]
    public async Task<ActionResult<IResult>> GetMosquesByName(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return new ErrorDataResult<string>(searchTerm, string.Format(Messages.DangerFieldIsEmpty, Dictionary.SearchTerm));
        if (searchTerm.Length < 3)
            return new ErrorDataResult<string>(searchTerm, string.Format(Messages.DangerFieldLengthLimit, Dictionary.SearchTerm, 3));

        return new SuccessDataResult<List<MosqueModel>>(await _placeService.GetMosquesByName(searchTerm));
    }

    [HttpGet("CitiesByName")]
    public async Task<ActionResult<IResult>> GetCitiesByName(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return new ErrorDataResult<string>(searchTerm, string.Format(Messages.DangerFieldIsEmpty, Dictionary.SearchTerm));
        if (searchTerm.Length < 3)
            return new ErrorDataResult<string>(searchTerm, string.Format(Messages.DangerFieldLengthLimit, Dictionary.SearchTerm, 3));

        return new SuccessDataResult<List<CityStateCountryWithLangModel>>(await _placeService.GetCitiesByName(searchTerm));
    }
}
