namespace DiyanetNamazVakti.Api.Service.Models;

public class CountryModel : IdCodeName<int>
{

    [Display(Name = nameof(Dictionary.Iso), ResourceType = typeof(Dictionary))]
    public string Iso { get; set; }
}
