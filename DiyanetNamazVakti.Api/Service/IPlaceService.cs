namespace DiyanetNamazVakti.Api.Service
{
    public interface IPlaceService
    {
        Task<List<IdCodeName<int>>> GetCountries();
        Task<List<IdCodeName<int>>> GetStates();
        Task<List<IdCodeName<int>>> GetStatesByCountry(int countryId);
        Task<List<IdCodeName<int>>> GetCities();
        Task<List<IdCodeName<int>>> GetCitiesByState(int stateId);
        Task<CityDetailModel> GetCity(int cityId);
    }
}