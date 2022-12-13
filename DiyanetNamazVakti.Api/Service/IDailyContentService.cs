namespace DiyanetNamazVakti.Api.Service;

public interface IDailyContentService
{
    Task<DailyContentModel> DailyContent();
}