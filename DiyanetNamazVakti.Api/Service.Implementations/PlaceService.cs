using DiyanetNamazVakti.Api.Core;
using DiyanetNamazVakti.Api.Core.Heplers;
using System.Net.Http.Headers;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace DiyanetNamazVakti.Api.Service.Implementations
{
    public class PlaceService : IPlaceService
    {
        private readonly ICacheService _cacheService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IAwqatSalahService _awqatSalahApiService;

        public PlaceService(ICacheService cacheService, IAwqatSalahService awqatSalahApiService)
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
                async () => await _awqatSalahApiService.GetAwqatSalahApiService<List<IdCodeName<int>>>("/api/Place/Countries", new CancellationToken()));
        }

        /// <summary>
        /// Eyaletler
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetStates()
        {
            return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
               async () => await _awqatSalahApiService.GetAwqatSalahApiService<List<IdCodeName<int>>>("/api/Place/States", new CancellationToken()));
        }

        /// <summary>
        /// Şehirler
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetCities()
        {
            return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
                async () => await _awqatSalahApiService.GetAwqatSalahApiService<List<IdCodeName<int>>>("/api/Place/Cities", new CancellationToken()));
        }

        /// <summary>
        /// İlgili Ülkenin Eyalet bilgilerini getirir.
        /// </summary>
        /// <param name="countryId">Ülke Id</param>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetStatesByCountry(int countryId)
        {
            return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + countryId,
                async () => await _awqatSalahApiService.GetAwqatSalahApiService<List<IdCodeName<int>>>($"/api/Place/States/{countryId}", new CancellationToken()));
        }

        /// <summary>
        /// İlgili Eyaletin, Şehir/İlçe bilgilerini getirir.
        /// </summary>
        /// <param name="stateId">Eyalet Id</param>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetCitiesByState(int stateId)
        {
            return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + stateId,
                async () => await _awqatSalahApiService.GetAwqatSalahApiService<List<IdCodeName<int>>>($"/api/Place/Cities/{stateId}", new CancellationToken()));
        }

        /// <summary>
        /// İlgili Şehir/İlçe bilgilerini, namaz vakitleri servisinden getirir.
        /// </summary>
        /// <param name="cityId">Şehir/İlçe</param>
        /// <returns></returns>
        public async Task<CityDetailModel> GetCity(int cityId)
        {
            return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + cityId,
                async () => await _awqatSalahApiService.GetAwqatSalahApiService<CityDetailModel>($"/api/Place/CityDetail/{cityId}", new CancellationToken()));
        }

        private async Task<List<CityModel>> GetAllCities()
        {
            return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
                async () => await _awqatSalahApiService.GetAwqatSalahApiService<List<CityModel>>($"/api/Place/Cities", new CancellationToken()));
        }
    }
}