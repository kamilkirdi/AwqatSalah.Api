namespace DiyanetNamazVakti.Api.Service.Models
{
    public class EidAwqatSalahModel
    {
        [Display(Name = nameof(Dictionary.EidAlAdhaDate), ResourceType = typeof(Dictionary))]
        public string EidAlAdhaHijri { get; set; }

        [Display(Name = nameof(Dictionary.EidAlAdhaTime), ResourceType = typeof(Dictionary))]
        public string EidAlAdhaTime { get; set; }

        [Display(Name = nameof(Dictionary.Country), ResourceType = typeof(Dictionary))]
        public string EidAlAdhaDate { get; set; }

        [Display(Name = nameof(Dictionary.Id), ResourceType = typeof(Dictionary))]
        public string EidAlFitrHijri { get; set; }

        [Display(Name = nameof(Dictionary.Country), ResourceType = typeof(Dictionary))]
        public string EidAlFitrTime { get; set; }

        [Display(Name = nameof(Dictionary.Country), ResourceType = typeof(Dictionary))]
        public string EidAlFitrDate { get; set; }
    }
}