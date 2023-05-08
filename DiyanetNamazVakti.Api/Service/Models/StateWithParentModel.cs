namespace DiyanetNamazVakti.Api.Service.Models;

public class StateWithParentModel
{
    [Display(Name = nameof(Dictionary.Id), ResourceType = typeof(Dictionary))]
    public int Id { get; set; }

    [Display(Name = nameof(Dictionary.Name), ResourceType = typeof(Dictionary))]
    public string Name { get; set; }

    [Display(Name = nameof(Dictionary.ForeignName), ResourceType = typeof(Dictionary))]
    public string EnglishName { get; set; }

    [Display(Name = nameof(Dictionary.Country), ResourceType = typeof(Dictionary))]
    public IdName<int> Country { get; set; }

}