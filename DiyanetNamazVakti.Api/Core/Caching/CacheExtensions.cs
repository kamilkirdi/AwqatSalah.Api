namespace DiyanetNamazVakti.Api.Core.Caching
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheService cacheService, string key, Func<T> acquire)
        {
            //eðer deðer daha önce cache edilmiþ ise deðeri getir 
            if (cacheService.Any(key))
            {
                return cacheService.Get<T>(key);
            }

            //deðer cache de yok ise ekle
            var result = acquire();
            cacheService.Add(key, result);
            return result;
        }
    }
}