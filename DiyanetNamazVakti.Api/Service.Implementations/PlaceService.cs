using DiyanetNamazVakti.Api.Core.Heplers;
using System.Net.Http.Headers;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace DiyanetNamazVakti.Api.Service.Implementations
{
    public class PlaceService : IPlaceService
    {
        private readonly ICacheService _cacheService;
        private readonly IAwqatSalahSettings _awqatSalahSettings;
        private readonly IHttpClientFactory _clientFactory;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            Converters = { new ObjectAsPrimitiveConverter(floatFormat: FloatFormat.Double, unknownNumberFormat: UnknownNumberFormat.Error, objectFormat: ObjectFormat.Dictionary) },
            WriteIndented = true,
        };

        public PlaceService(ICacheService cacheService, IHttpClientFactory clientFactory, IAwqatSalahSettings awqatSalahSettings)
        {
            _cacheService = cacheService;
            _clientFactory = clientFactory;
            _awqatSalahSettings = awqatSalahSettings;
        }

        /// <summary>
        /// Ülkeler
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetCountries(CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient("AwqatSalahApi");
            var request = string.Format("Auth/Login");

            //client.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer ");
            var todoItemJson = new StringContent(JsonSerializer.Serialize(new LoginModel() { Password = _awqatSalahSettings.Password, Email = _awqatSalahSettings.UserName }), Encoding.UTF8, Application.Json);
            var result = await client.PostAsync(request, todoItemJson, cancellationToken: cancellationToken);
            if (result.IsSuccessStatusCode)
            {
                using var stream = await result.Content.ReadAsStreamAsync();
                using var jsonDoc = await JsonDocument.ParseAsync(stream);
                var token = jsonDoc.RootElement.GetProperty("data").Deserialize<TokenModel>(JsonConstants.SerializerOptions);
                if (!client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                }
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Eyaletler
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetStates()
        {
            //return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            //    async () => await _context.Set<State>().AsNoTracking().Select(x => new IdCodeName<int>()
            //    {
            //        Id = x.StateId,
            //        Name = x.StateName,
            //        Code = x.EnStateName

            //    }).ToListAsync());
            throw new NotImplementedException();

        }

        /// <summary>
        /// Şehirler
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetCities()
        {
            //return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            //    async () => await _context.Set<City>().AsNoTracking().Select(x => new IdCodeName<int>()
            //    {
            //        Id = x.CityId,
            //        Name = x.CityName,
            //        Code = x.EnCityName
            //    }).ToListAsync());
            throw new NotImplementedException();


        }

        /// <summary>
        /// İlgili Ülkenin Eyalet bilgilerini getirir.
        /// </summary>
        /// <param name="countryId">Ülke Id</param>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetStatesByCountry(int countryId)
        {
            //return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + countryId,
            //    async () => await _context.Set<State>().AsNoTracking().Where(x => x.CountryId == countryId).Select(x => new IdCodeName<int>()
            //    {
            //        Id = x.StateId,
            //        Name = x.StateName,
            //        Code = x.EnStateName

            //    }).ToListAsync());
            throw new NotImplementedException();
        }

        /// <summary>
        /// İlgili Eyaletin, Şehir/İlçe bilgilerini getirir.
        /// </summary>
        /// <param name="stateId">Eyalet Id</param>
        /// <returns></returns>
        public async Task<List<IdCodeName<int>>> GetCitiesByState(int stateId)
        {
            //return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + stateId,
            //    async () => await _context.Set<City>().AsNoTracking().Where(x => x.StateId == stateId).Select(x => new IdCodeName<int>()
            //    {
            //        Id = x.CityId,
            //        Name = x.CityName,
            //        Code = x.EnCityName

            //    }).ToListAsync());
            throw new NotImplementedException();
        }

        /// <summary>
        /// İlgili Şehir/İlçe bilgilerini, namaz vakitleri servisinden getirir.
        /// </summary>
        /// <param name="cityId">Şehir/İlçe</param>
        /// <returns></returns>
        public async Task<CityDetailModel> GetCity(int cityId)
        {
            //return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + cityId,
            //    async () => await _prayerTimeService.CityDetail(cityId));
            throw new NotImplementedException();
        }


        private async Task<List<CityModel>> GetAllCities()
        {
            //return await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            //    async () => await _context.Set<City>().AsNoTracking().Select(x => new CityModel()
            //    {
            //        Id = x.CityId,
            //        Name = x.CityName,
            //        EnName = x.EnCityName,
            //        Latitude = x.Latitude,
            //        Longitude = x.Longitude
            //    }).ToListAsync());
            throw new NotImplementedException();

        }



    }
}