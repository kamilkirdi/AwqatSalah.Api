namespace DiyanetNamazVakti.Api.Core.Exceptions;
public class ValidationErrorModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}
