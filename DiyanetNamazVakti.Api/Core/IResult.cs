namespace DiyanetNamazVakti.Api.Core;

public interface IResult
{
    [JsonPropertyName("success")]
    bool Success { get; }

    [JsonPropertyName("message")]
    string Message { get; }
}