namespace DiyanetNamazVakti.Api.Service;

public interface IAwqatSalahService
{
    Task<List<AwqatSalahModel>> DailyAwqatSalah(int cityId);
    Task<List<AwqatSalahModel>> WeeklyAwqatSalah(int cityId);
    Task<List<AwqatSalahModel>> MonthlyAwqatSalah(int cityId);
    Task<EidAwqatSalahModel> EidAwqatSalah(int cityId);
    Task<List<AwqatSalahModel>> RamadanAwqatSalah(int cityId);
}