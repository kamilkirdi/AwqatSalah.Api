namespace DiyanetNamazVakti.Api.Service.Implementations;

public class DailyContentService : IDailyContentService
{
    private readonly ICacheService _cacheService;
    private readonly IAwqatSalahConnectService _awqatSalahApiService;

    public DailyContentService(ICacheService cacheService, IAwqatSalahConnectService awqatSalahApiService)
    {
        _cacheService = cacheService;
        _awqatSalahApiService = awqatSalahApiService;
    }

    public async Task<DailyContentModel> DailyContent()
    {
        var result = await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!,
            async () => await _awqatSalahApiService.CallService<DailyContentModel>("/api/DailyContent",
            MethodOption.Get, null, new CancellationToken()),
        DateTime.Now.ResetTimeToEndOfDay());

        if (result is null)
            _cacheService.Remove(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!);

        return result;
    }
}