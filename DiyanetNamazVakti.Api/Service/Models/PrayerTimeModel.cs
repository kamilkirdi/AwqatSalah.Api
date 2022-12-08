namespace DiyanetNamazVakti.Api.Service.Models
{
    public class AwqatSalahModel
    {
        [Display(Name = nameof(Dictionary.ShapeMoon), ResourceType = typeof(Dictionary))]
        public string ShapeMoonUrl { get; set; }

        [Display(Name = nameof(Dictionary.Fajr), ResourceType = typeof(Dictionary))]
        public string Fajr { get; set; }

        [Display(Name = nameof(Dictionary.Sunrise), ResourceType = typeof(Dictionary))]
        public string Sunrise { get; set; }

        [Display(Name = nameof(Dictionary.Dhuhr), ResourceType = typeof(Dictionary))]
        public string Dhuhr { get; set; }

        [Display(Name = nameof(Dictionary.Asr), ResourceType = typeof(Dictionary))]
        public string Asr { get; set; }

        [Display(Name = nameof(Dictionary.Maghrib), ResourceType = typeof(Dictionary))]
        public string Maghrib { get; set; }

        [Display(Name = nameof(Dictionary.Isha), ResourceType = typeof(Dictionary))]
        public string Isha { get; set; }

        [Display(Name = nameof(Dictionary.AstronomicalSunset), ResourceType = typeof(Dictionary))]
        public string AstronomicalSunset { get; set; }

        [Display(Name = nameof(Dictionary.AstronomicalSunrise), ResourceType = typeof(Dictionary))]
        public string AstronomicalSunrise { get; set; }

        [Display(Name = nameof(Dictionary.HijriDate), ResourceType = typeof(Dictionary))]
        public string HijriDateShort { get; set; }

        [Display(Name = nameof(Dictionary.HijriDate), ResourceType = typeof(Dictionary))]
        public string HijriDateShortIso8601 { get; set; }

        [Display(Name = nameof(Dictionary.HijriDate), ResourceType = typeof(Dictionary))]
        public string HijriDateLong { get; set; }

        [Display(Name = nameof(Dictionary.HijriDate), ResourceType = typeof(Dictionary))]
        public string HijriDateLongIso8601 { get; set; }

        [Display(Name = nameof(Dictionary.QiblaTime), ResourceType = typeof(Dictionary))]
        public string QiblaTime { get; set; }

        [Display(Name = nameof(Dictionary.GregorianDate), ResourceType = typeof(Dictionary))]
        public string GregorianDateShort { get; set; }

        [Display(Name = nameof(Dictionary.GregorianDate), ResourceType = typeof(Dictionary))]
        public string GregorianDateShortIso8601 { get; set; }

        [Display(Name = nameof(Dictionary.GregorianDate), ResourceType = typeof(Dictionary))]
        public string GregorianDateLong { get; set; }

        [Display(Name = nameof(Dictionary.GregorianDate), ResourceType = typeof(Dictionary))]
        public string GregorianDateLongIso8601 { get; set; }
    }
}