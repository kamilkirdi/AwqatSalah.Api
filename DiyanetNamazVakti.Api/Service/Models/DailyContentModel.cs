namespace DiyanetNamazVakti.Api.Service.Models
{
    public partial class DailyContentModel
    {
        [Display(Name = nameof(Dictionary.Id), ResourceType = typeof(Dictionary))]
        public int Id { get; set; }

        [Display(Name = nameof(Dictionary.DayOfYear), ResourceType = typeof(Dictionary))]
        public double? DayOfYear { get; set; }

        [Display(Name = nameof(Dictionary.Verse), ResourceType = typeof(Dictionary))]
        public string Verse { get; set; }

        [Display(Name = nameof(Dictionary.VerseSource), ResourceType = typeof(Dictionary))]
        public string VerseSource { get; set; }

        [Display(Name = nameof(Dictionary.Hadith), ResourceType = typeof(Dictionary))]
        public string Hadith { get; set; }

        [Display(Name = nameof(Dictionary.HadithSource), ResourceType = typeof(Dictionary))]
        public string HadithSource { get; set; }

        [Display(Name = nameof(Dictionary.Pray), ResourceType = typeof(Dictionary))]
        public string Pray { get; set; }

        [Display(Name = nameof(Dictionary.PraySource), ResourceType = typeof(Dictionary))]
        public string PraySource { get; set; }
    }
}