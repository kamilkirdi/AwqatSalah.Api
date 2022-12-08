namespace DiyanetNamazVakti.Api.Service.Models
{
    public class CityDetailModel
    {
        [Display(Name = nameof(Dictionary.Id), ResourceType = typeof(Dictionary))]
        public string Id { get; set; }

        [Display(Name = nameof(Dictionary.Name), ResourceType = typeof(Dictionary))]
        public string Name { get; set; }

        public string Code { get; set; }

        [Display(Name = nameof(Dictionary.GeographicQiblaAngle), ResourceType = typeof(Dictionary))]
        public string GeographicQiblaAngle { get; set; }

        [Display(Name = nameof(Dictionary.DistanceToKaaba), ResourceType = typeof(Dictionary))]
        public string DistanceToKaaba { get; set; }

        [Display(Name = nameof(Dictionary.QiblaAngle), ResourceType = typeof(Dictionary))]
        public string QiblaAngle { get; set; }

        [Display(Name = nameof(Dictionary.City), ResourceType = typeof(Dictionary))]
        public string City { get; set; }

        [Display(Name = nameof(Dictionary.City), ResourceType = typeof(Dictionary))]
        public string CityEn { get; set; }

        [Display(Name = nameof(Dictionary.Country), ResourceType = typeof(Dictionary))]
        public string Country { get; set; }

        [Display(Name = nameof(Dictionary.Country), ResourceType = typeof(Dictionary))]
        public string CountryEn { get; set; }
    }
}