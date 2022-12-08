namespace DiyanetNamazVakti.Api.Service;

public interface IAwqatSalahService
{
    Task<T> GetAwqatSalahApiService<T>(string path, CancellationToken cancellationToken) where T : class, new();
}