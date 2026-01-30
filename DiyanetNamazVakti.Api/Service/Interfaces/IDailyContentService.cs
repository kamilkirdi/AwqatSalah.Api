namespace DiyanetNamazVakti.Api.Service.Interfaces;

public interface IDailyContentService
{
    Task<DailyContentModel> DailyContent();
}