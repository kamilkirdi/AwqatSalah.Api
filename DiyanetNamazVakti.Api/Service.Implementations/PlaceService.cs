namespace DiyanetNamazVakti.Api.Service.Implementations;

public class PlaceService : IPlaceService
{
    private readonly ICacheService _cacheService;
    private readonly IAwqatSalahConnectService _awqatSalahApiService;

    public PlaceService(ICacheService cacheService, IAwqatSalahConnectService awqatSalahApiService)
    {
        _cacheService = cacheService;
        _awqatSalahApiService = awqatSalahApiService;
    }

    /// <summary>
    /// Ülkeler
    /// </summary>
    /// <returns></returns>
    public async Task<List<IdCodeName<int>>> GetCountries()
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            async () => await _awqatSalahApiService.CallService<List<IdCodeName<int>>>("/api/Place/Countries", MethodOption.Get, null, new CancellationToken()));
    }


    /// <summary>
    /// Ülkeler V2
    /// </summary>
    /// <returns></returns>
    public async Task<List<CountryModel>> GetCountriesV2()
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            async () => await _awqatSalahApiService.CallService<List<CountryModel>>("/api/v2/Place/Countries", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// Eyaletler
    /// </summary>
    /// <returns></returns>
    public async Task<List<IdCodeName<int>>> GetStates()
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
           async () => await _awqatSalahApiService.CallService<List<IdCodeName<int>>>("/api/Place/States", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// Eyaletler V2
    /// </summary>
    /// <returns></returns>
    public async Task<List<StateWithParentModel>> GetStatesV2()
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
           async () => await _awqatSalahApiService.CallService<List<StateWithParentModel>>("/api/v2/Place/States", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// Şehirler
    /// </summary>
    /// <returns></returns>
    public async Task<List<IdCodeName<int>>> GetCities()
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            async () => await _awqatSalahApiService.CallService<List<IdCodeName<int>>>("/api/Place/Cities", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// Şehirler V2
    /// </summary>
    /// <returns></returns>
    public async Task<List<CityWithParentsModel>> GetCitiesV2()
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            async () => await _awqatSalahApiService.CallService<List<CityWithParentsModel>>("api/v2/Place/Cities", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// İlgili Ülkenin Eyalet bilgilerini getirir.
    /// </summary>
    /// <param name="countryId">Ülke Id</param>
    /// <returns></returns>
    public async Task<List<IdCodeName<int>>> GetStatesByCountry(int countryId)
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + countryId,
            async () => await _awqatSalahApiService.CallService<List<IdCodeName<int>>>($"/api/Place/States/{countryId}", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// İlgili Ülkenin Eyalet bilgilerini getirir. V2
    /// </summary>
    /// <param name="countryId">Ülke Id</param>
    /// <returns></returns>
    public async Task<List<StateWithParentModel>> GetStatesByCountryV2(int countryId)
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + countryId,
            async () => await _awqatSalahApiService.CallService<List<StateWithParentModel>>($"/api/v2/Place/States/{countryId}", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// İlgili Eyaletin, Şehir/İlçe bilgilerini getirir.
    /// </summary>
    /// <param name="stateId">Eyalet Id</param>
    /// <returns></returns>
    public async Task<List<IdCodeName<int>>> GetCitiesByState(int stateId)
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + stateId,
            async () => await _awqatSalahApiService.CallService<List<IdCodeName<int>>>($"/api/Place/Cities/{stateId}", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// İlgili Eyaletin, Şehir/İlçe bilgilerini getirir. V2
    /// </summary>
    /// <param name="stateId">Eyalet Id</param>
    /// <returns></returns>
    public async Task<List<CityWithParentsModel>> GetCitiesByStateV2(int stateId)
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + stateId,
            async () => await _awqatSalahApiService.CallService<List<CityWithParentsModel>>($"/api/v2/Place/Cities/{stateId}", MethodOption.Get, null, new CancellationToken()));
    }

    /// <summary>
    /// İlgili Şehir/İlçe bilgilerini, namaz vakitleri servisinden getirir.
    /// </summary>
    /// <param name="cityId">Şehir/İlçe</param>
    /// <returns></returns>
    public async Task<CityDetailModel> GetCity(int cityId)
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + cityId,
            async () => await _awqatSalahApiService.CallService<CityDetailModel>($"/api/Place/CityDetail/{cityId}", MethodOption.Get, null, new CancellationToken()));
    }

    private async Task<List<CityModel>> GetAllCities()
    {
        return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            async () => await _awqatSalahApiService.CallService<List<CityModel>>($"/api/Place/Cities", MethodOption.Get, null, new CancellationToken()));
    }
}