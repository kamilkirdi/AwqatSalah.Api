using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;

namespace DiyanetNamazVakti.Api.Service;

public class AwqatSalahApiService : IAwqatSalahConnectService
{
    private readonly ICacheService _cacheService;
    private readonly HttpClient _client;
    private readonly IAwqatSalahSettings _awqatSalahSettings;

    public AwqatSalahApiService(ICacheService cacheService, IHttpClientFactory clientFactory, IAwqatSalahSettings awqatSalahSettings)
    {
        _cacheService = cacheService;
        _awqatSalahSettings = awqatSalahSettings;
        _client = clientFactory.CreateClient("AwqatSalahApi");
    }

    public async Task<T> GetAwqatSalahApiService<T>(string path, CancellationToken cancellationToken) where T : class, new()
    {
        if (string.IsNullOrEmpty(path))
            ArgumentNullException.ThrowIfNull(path);

        var request = string.Format(path);

        await AddTokenToHeader(cancellationToken);

        var result = await _client.GetAsync(request, cancellationToken: cancellationToken);
        if (result.IsSuccessStatusCode)
        {
            using var stream = await result.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(stream);
            return jsonDoc.RootElement.GetProperty("data").Deserialize<T>(JsonConstants.SerializerOptions)!;
        }
        return new T();
    }

    private async Task AddTokenToHeader(CancellationToken cancellationToken)
    {
            var token = await _cacheService.GetOrCreateAsync(nameof(CacheNameConstants.TokenCacheName), async () => await AwqatSalahLogin(cancellationToken), DateTime.Now.AddMinutes(_awqatSalahSettings.TokenLifetimeMinutes));

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }

            if (token.ExpireTime <= DateTime.Now)
            {
                if ((DateTime.Now - token.ExpireTime).TotalMinutes < 15)
                {
                    _cacheService.Remove(CacheNameConstants.TokenCacheName);
                    token = await _cacheService.GetOrCreateAsync(nameof(CacheNameConstants.TokenCacheName),
                        async () => await AwqatSalahRefreshToken(token.RefreshToken, cancellationToken), DateTime.Now.AddMinutes(_awqatSalahSettings.TokenLifetimeMinutes));

                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                }
                else
                {
                    _cacheService.Remove(CacheNameConstants.TokenCacheName);
                    await AddTokenToHeader(cancellationToken);
                }
            }
            else if ((DateTime.Now - token.ExpireTime).TotalSeconds < 1)
            {
                _cacheService.Remove(CacheNameConstants.TokenCacheName);
                token = await _cacheService.GetOrCreateAsync(nameof(CacheNameConstants.TokenCacheName),
                    async () => await AwqatSalahRefreshToken(token.RefreshToken, cancellationToken), DateTime.Now.AddMinutes(_awqatSalahSettings.TokenLifetimeMinutes));

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }
    }

    private async Task<TokenWithExpireModel> AwqatSalahLogin(CancellationToken cancellationToken)
    {
            var loginCredential = new StringContent(JsonSerializer.Serialize(new LoginModel() { Password = _awqatSalahSettings.Password, Email = _awqatSalahSettings.UserName }), Encoding.UTF8, Application.Json);

            var resultAuth = await _client.PostAsync("Auth/Login", loginCredential, cancellationToken: cancellationToken);

            if (!resultAuth.IsSuccessStatusCode)
                throw new HttpRequestException(resultAuth.StatusCode.ToString());

            using var stream = await resultAuth.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(stream);
            var token = jsonDoc.RootElement.GetProperty("data").Deserialize<TokenWithExpireModel>(JsonConstants.SerializerOptions)!;
            token.ExpireTime = DateTime.Now.AddMinutes(_awqatSalahSettings.TokenLifetimeMinutes);
            return token;
    }

    private async Task<TokenWithExpireModel> AwqatSalahRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
            var resultAuth = await _client.GetAsync($"Auth/RefreshToken/{refreshToken}", cancellationToken: cancellationToken);
            if (!resultAuth.IsSuccessStatusCode)
                throw new HttpRequestException(resultAuth.StatusCode.ToString());

            using var stream = await resultAuth.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(stream);
            var token = jsonDoc.RootElement.GetProperty("data").Deserialize<TokenWithExpireModel>(JsonConstants.SerializerOptions)!;
            token.ExpireTime = DateTime.Now.AddMinutes(_awqatSalahSettings.TokenLifetimeMinutes);
            return token;
        }
}
