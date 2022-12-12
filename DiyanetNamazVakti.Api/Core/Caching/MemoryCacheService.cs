using DiyanetNamazVakti.Api.Core.Heplers;

namespace DiyanetNamazVakti.Api.Core.Caching
{
    /// <inheritdoc />
    /// <summary>
    /// RAM önbellekleme iþlemlerini yapan sýnýf
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        private readonly ICacheSettings _cacheSettings;
        private object _lock = new object();

        private readonly IMemoryCache _cache;
        public MemoryCacheService(IMemoryCache cache, ICacheSettings cacheSettings)
        {
            _cache = cache;
            _cacheSettings = cacheSettings;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> generatorAsync, DateTime expiredTime = default)
        {
            var cacheEntry = await _cache.GetOrCreateAsync(key, async entry =>
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
            lock (_lock)
            {
                _cache.Remove(key);
                RemoveFromKeys(key);
            }
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

            _cache.Remove(_cacheSettings.MainKey);
            _cache.Set(_cacheSettings.MainKey, newKeys.SerializeToString().Encrypt(), DateTimeOffset.Now.AddMinutes(_cacheSettings.ExpiryTime));
        }

        public List<string> GetAllKeys()
        {
            lock (_lock)
            {
                var list = new List<string>();
                if (Any(_cacheSettings.MainKey))
                {
                    list = Get<List<string>>(_cacheSettings.MainKey);
                }
                return list;
            }
        }

        private void AddToKeys(string key)
        {
            lock (_lock)
            {
                var keyList = GetAllKeys();
                if (!keyList.Contains(key))
                {
                    keyList.Add(key);
                }
                _cache.Remove(_cacheSettings.MainKey);
                _cache.Set(_cacheSettings.MainKey, keyList.SerializeToString().Encrypt(), DateTimeOffset.Now.AddMinutes(_cacheSettings.ExpiryTime));
            }
        }

        private void RemoveFromKeys(string key)
        {
            lock (_lock)
            {
                var keyList = GetAllKeys();

                if (keyList.Contains(key))
                {
                    keyList.Remove(key);
                }
                _cache.Remove(_cacheSettings.MainKey);
                _cache.Set(_cacheSettings.MainKey, keyList.SerializeToString().Encrypt(), DateTimeOffset.Now.AddMinutes(_cacheSettings.ExpiryTime));
            }
        }

        public T Get<T>(string key)
        {
            return _cache.Get<string>(key)!.Decrypt().DeserializeFromString<T>();
        }

        public void Add(string key, object value)
        {
            // Deðer boþ ise ekleme yapýlmaz
            if (value == null)
            {
                return;
            }

            // Anahtar daha öce kullanýldý ise ekleme yapýlmaz
            if (Any(key))
            {
                return;
            }

            // Kayýt eklenir
            _cache.Set(key, value.SerializeToString().Encrypt(), DateTimeOffset.Now.AddMinutes(_cacheSettings.ExpiryTime));
            AddToKeys(key);
        }
    }
}