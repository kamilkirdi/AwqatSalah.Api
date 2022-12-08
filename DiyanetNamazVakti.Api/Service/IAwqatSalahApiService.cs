namespace DiyanetNamazVakti.Api.Service;

public interface IAwqatSalahConnectService
{
    Task<T> GetAwqatSalahApiService<T>(string path, CancellationToken cancellationToken) where T : class, new();
}