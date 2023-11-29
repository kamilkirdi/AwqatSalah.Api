using DiyanetNamazVakti.Api.Core.ValueObjects;
using PrayerTime.Service.Models;

namespace DiyanetNamazVakti.Api.Service.Implementations;

public class AwqatSalahService : IAwqatSalahService
{
    private readonly IAwqatSalahSettings _namazVaktiSettings;
    private readonly ICacheService _cacheService;
    private readonly IAwqatSalahConnectService _awqatSalahApiService;

    public AwqatSalahService(IAwqatSalahSettings namazVaktiSettings, ICacheService cacheService, IAwqatSalahConnectService awqatSalahApiService)
    {
        _namazVaktiSettings = namazVaktiSettings;
        _cacheService = cacheService;
        _awqatSalahApiService = awqatSalahApiService;
    }

    public async Task<List<AwqatSalahModel>> DailyAwqatSalah(int cityId)
    {
        var result = await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId,
            async () => await _awqatSalahApiService.CallService<List<AwqatSalahModel>>("/api/PrayerTime/Daily/" + cityId,
            MethodOption.Get, null, new CancellationToken()),
        DateTime.Now.ResetTimeToEndOfDay());

        if (result is null)
            _cacheService.Remove(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId);

        return result;
    }

    public async Task<List<AwqatSalahModel>> MonthlyAwqatSalah(int cityId)
    {
        var result = await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId,
                   async () => await _awqatSalahApiService.CallService<List<AwqatSalahModel>>("/api/PrayerTime/Monthly/" + cityId,
                   MethodOption.Get, null, new CancellationToken()),
        DateTime.Now.AddDays(30).ResetTimeToEndOfDay());

        if (result is null)
            _cacheService.Remove(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId);

        return result;
    }

    public async Task<List<AwqatSalahModel>> YearlyAwqatSalah(DateRangeFilter dateRange)
    {
        if (dateRange.StartDate > dateRange.EndDate)
            throw new InvalidTransactionException(Dictionary.StartDateNotAvailable);

        var cacheKey = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + "." + dateRange.CityId + "." + dateRange.StartDate.ToShortDateString() + "." + dateRange.EndDate.ToShortDateString();
        var result = await _cacheService.GetOrCreateAsync(cacheKey,
            async () => await _awqatSalahApiService.CallService<List<AwqatSalahModel>>("/api/PrayerTime/DateRange",
                   MethodOption.Post, dateRange, new CancellationToken()),
        dateRange.EndDate.ResetTimeToEndOfDay());

        if (result is null)
            _cacheService.Remove(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + dateRange.CityId);

        return result;
    }

    public async Task<List<AwqatSalahModel>> WeeklyAwqatSalah(int cityId)
    {
        var result = await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId,
             async () => await _awqatSalahApiService.CallService<List<AwqatSalahModel>>("/api/PrayerTime/Weekly/" + cityId,
             MethodOption.Get, null, new CancellationToken()),
        DateTime.Now.ResetTimeToEndOfDay());

        if (result is null)
            _cacheService.Remove(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId);

        return result;
    }
    public async Task<List<AwqatSalahModel>> RamadanAwqatSalah(int cityId)
    {
        var result = await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId,
             async () => await _awqatSalahApiService.CallService<List<AwqatSalahModel>>("/api/PrayerTime/Ramadan/" + cityId,
             MethodOption.Get, null, new CancellationToken()));
        if (result is null)
            _cacheService.Remove(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId);

        return result;
    }

    public async Task<EidAwqatSalahModel> EidAwqatSalah(int cityId)
    {
        var result = await _cacheService.GetOrCreateAsync(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId,
             async () => await _awqatSalahApiService.CallService<EidAwqatSalahModel>("/api/PrayerTime/Eid/" + cityId,
             MethodOption.Get, null, new CancellationToken()),
        DateTime.Now.ResetTimeToEndOfDay());

        if (result is null)
            _cacheService.Remove(MethodBase.GetCurrentMethod()!.DeclaringType!.FullName! + cityId);

        return result;
    }
}