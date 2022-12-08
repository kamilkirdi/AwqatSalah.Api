
namespace DiyanetNamazVakti.Api.Service
{
    public interface IPrayerTimeService
    {
        Task<List<AwqatSalahModel>> DailyPrayerTime(int cityId);
        Task<List<AwqatSalahModel>> WeeklyPrayerTime(int cityId);
        Task<List<AwqatSalahModel>> MonthlyPrayerTime(int cityId);
        Task<EidAwqatSalahModel> EidPrayerTime(int cityId);
        Task<List<IdCodeName<int>>> Country();
        Task<List<IdCodeName<int>>> State(int countryId);
        Task<List<IdCodeName<int>>> City(int stateId);
        Task<CityDetailModel> CityDetail(int cityId);
    }
}