namespace DiyanetNamazVakti.Api.Service.Models
{
    public class CityModel
    {
        [Display(Name = nameof(Dictionary.Id), ResourceType = typeof(Dictionary))]
        public int Id { get; set; }

        [Display(Name = nameof(Dictionary.Name), ResourceType = typeof(Dictionary))]
        public string Name { get; set; }

        [Display(Name = nameof(Dictionary.ForeignName), ResourceType = typeof(Dictionary))]
        public string EnName { get; set; }

        [Display(Name = nameof(Dictionary.Latitude), ResourceType = typeof(Dictionary))]
        public double Latitude { get; set; }

        [Display(Name = nameof(Dictionary.Longitude), ResourceType = typeof(Dictionary))]
        public double Longitude { get; set; }
    }
}