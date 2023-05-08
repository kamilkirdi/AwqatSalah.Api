namespace DiyanetNamazVakti.Api.Service;

public interface IPlaceService
{
    Task<List<IdCodeName<int>>> GetCountries();
    Task<List<CountryModel>> GetCountriesV2();
    Task<List<IdCodeName<int>>> GetStates();
    Task<List<StateWithParentModel>> GetStatesV2();
    Task<List<IdCodeName<int>>> GetStatesByCountry(int countryId);
    Task<List<StateWithParentModel>> GetStatesByCountryV2(int countryId);
    Task<List<IdCodeName<int>>> GetCities();
    Task<List<CityWithParentsModel>> GetCitiesV2();
    Task<List<IdCodeName<int>>> GetCitiesByState(int stateId);
    Task<List<CityWithParentsModel>> GetCitiesByStateV2(int stateId);
    Task<CityDetailModel> GetCity(int cityId);
}