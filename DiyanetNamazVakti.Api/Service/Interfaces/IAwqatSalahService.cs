namespace DiyanetNamazVakti.Api.Service.Interfaces;

public interface IAwqatSalahService
{
    Task<List<AwqatSalahModel>> DailyAwqatSalah(int cityId);
    Task<List<AwqatSalahModel>> WeeklyAwqatSalah(int cityId);
    Task<List<AwqatSalahModel>> MonthlyAwqatSalah(int cityId);
    Task<List<AwqatSalahModel>> YearlyAwqatSalah(DateRangeFilter dateRange);
    Task<EidAwqatSalahModel> EidAwqatSalah(int cityId);
    Task<List<AwqatSalahModel>> RamadanAwqatSalah(int cityId);
}