namespace PrayerTime.Service
{
    public interface IDailyContentService
    {
        Task<DailyContentModel> DailyContent();
    }
}