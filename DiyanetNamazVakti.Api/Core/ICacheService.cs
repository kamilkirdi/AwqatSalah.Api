namespace DiyanetNamazVakti.Api.Core
{
    public interface ICacheService
    {
        /// <summary>
        /// Önbelleðe deðer ekler
        /// </summary>
        /// <typeparam name="T">Eklenecek veya Getirilecek verinin tipi</typeparam>
        /// <param name="key">Anahtar Kelime</param>
        /// <param name="generatorAsync">Cachede olmamasý durumunda verinin alýnacaðý Asenkron (Zaman Uyumsuz) metod</param>
        /// <returns></returns>
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> generatorAsync, DateTime expiredTime = default);

        /// <summary>
        /// Önbellekteki deðeri istenilen türde döndürür.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Anahtar</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Önbellekte olup olmadýðýný kontrol eder.
        /// </summary>
        /// <param name="key">Anahtar</param>
        /// <returns></returns>
        bool Any(string key);

        /// <summary>
        /// Önbelleðe deðer ekler
        /// </summary>
        /// <param name="key">Anahtar</param>
        /// <param name="value">Deðer</param>
        void Add(string key, object value);

        /// <summary>
        /// Önbellekteki kaydý siler.
        /// </summary>
        /// <param name="key">Anahtar</param>
        void Remove(string key);

        /// <summary>
        /// Önbellekteki tüm kaydý siler.
        /// </summary>
        void Clear();

        /// <summary>
        /// Önbellekteki parametre ile baþlayan tüm kaydý siler.
        /// </summary>
        /// <param name="prefix">Anahtar</param>
        void StartsWithClear(string prefix);

        /// <summary>
        /// Önbellekteki anahtar listesini döndürür.
        /// </summary>
        List<string> GetAllKeys();
    }
}