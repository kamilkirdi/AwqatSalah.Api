using DiyanetNamazVakti.Api.Core.ValueObjects;

namespace DiyanetNamazVakti.Api.Service.Models;

public class CityWithParentsModel
{
    [Display(Name = nameof(Dictionary.Id), ResourceType = typeof(Dictionary))]
    public int Id { get; set; }

    [Display(Name = nameof(Dictionary.Name), ResourceType = typeof(Dictionary))]
    public string Name { get; set; }

    [Display(Name = nameof(Dictionary.ForeignName), ResourceType = typeof(Dictionary))]
    public string NormalizedName { get; set; }

    [Display(Name = nameof(Dictionary.State), ResourceType = typeof(Dictionary))]
    public IdName<int> State { get; set; }

    [Display(Name = nameof(Dictionary.Country), ResourceType = typeof(Dictionary))]
    public IdCodeName<int> Country { get; set; }
}
