namespace DiyanetNamazVakti.Api.Core.Caching;

/// <inheritdoc />
/// <summary>
/// RAM önbellekleme iþlemlerini yapan sýnýf
/// </summary>
public class MemoryCacheService : ICacheService
{
    private readonly ICacheSettings _cacheSettings;
    private readonly Lazy<List<string>> _cacheKeys = new();

    private readonly IMemoryCache _cache;
    public MemoryCacheService(IMemoryCache cache, ICacheSettings cacheSettings)
    {
        _cache = cache;
        _cacheSettings = cacheSettings;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> generatorAsync, DateTime expiredTime = default)
    {
        var cacheEntry = await _cache.GetOrCreateAsync<T>(key, async entry =>
        {
            AddToKeys(key);
            entry.AbsoluteExpiration = expiredTime == default ? DateTime.Now.AddDays(_cacheSettings.ExpiryTime) : expiredTime;
            return await generatorAsync();
        });
        return cacheEntry!;
    }

    public bool Any(string key)
    {
        return _cache.TryGetValue(key, out var o);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
        RemoveFromKeys(key);
    }

    public void Clear()
    {
        foreach (var key in GetAllKeys())
        {
            _cache.Remove(key);
        }
        _cache.Remove(_cacheSettings.MainKey);
    }

    public void StartsWithClear(string prefix)
    {
        var newKeys = new List<string>();
        var deletedKeys = new List<string>();
        foreach (var key in GetAllKeys())
        {
            if (key.StartsWith(prefix))
            {
                deletedKeys.Add(key);
            }
            else
            {
                newKeys.Add(key);
            }
        }

        foreach (var key in deletedKeys)
        {
            _cache.Remove(key);
        }
    }

    public List<string> GetAllKeys()
    {
        return _cacheKeys.Value;
    }

    private void AddToKeys(string key)
    {
        var keyList = GetAllKeys();
        if (!keyList.Any(x => x == key))
            keyList.Add(key);
    }

    private void RemoveFromKeys(string key)
    {
        var keyList = GetAllKeys();

        if (keyList.Any(x => x == key))
            keyList.Remove(key);
    }

    public T Get<T>(string key)
    {
        return _cache.Get<string>(key)!.Decrypt().DeserializeFromString<T>();
    }

    public void Add(string key, object value)
    {
        // Deðer boþ ise ekleme yapýlmaz
        if (value == null) return;

        // Anahtar daha öce kullanýldý ise ekleme yapýlmaz
        if (Any(key)) return;

        // Kayýt eklenir
        _cache.Set(key, value.SerializeToString().Encrypt(), DateTimeOffset.Now.AddMinutes(_cacheSettings.ExpiryTime));
        AddToKeys(key);
    }
}
