using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;

namespace DiyanetNamazVakti.Api.Service;

public class AwqatSalahApiService : IAwqatSalahService
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

        await AddToken(cancellationToken);

        var result = await _client.GetAsync(request, cancellationToken: cancellationToken);
        if (result.IsSuccessStatusCode)
        {
            using var stream = await result.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(stream);
            return jsonDoc.RootElement.GetProperty("data").Deserialize<T>(JsonConstants.SerializerOptions)!;
        }
        return new T();
    }

    private async Task AddToken(CancellationToken cancellationToken)
    {
        var token = await _cacheService.GetOrCreateAsync(nameof(CacheNameConstants.TokenCacheName), async () => await AwqatSalahLogin(cancellationToken), DateTime.Now.AddMinutes(45));
        if (!_client.DefaultRequestHeaders.Contains("Authorization"))
        {
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
        return jsonDoc.RootElement.GetProperty("data").Deserialize<TokenWithExpireModel>(JsonConstants.SerializerOptions)!;
    }
}
