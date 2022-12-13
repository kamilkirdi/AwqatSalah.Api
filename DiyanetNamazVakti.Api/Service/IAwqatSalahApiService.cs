namespace DiyanetNamazVakti.Api.Service;

public interface IAwqatSalahConnectService
{
    Task<T> CallService<T>(string path, MethodOption method, object model, CancellationToken cancellationToken) where T : class, new();
}