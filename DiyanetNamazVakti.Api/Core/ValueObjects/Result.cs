namespace DiyanetNamazVakti.Api.Core.ValueObjects;

public class Result : IResult
{
    public Result(bool success, string message) : this(success)
    {
        Message = message;
    }

    public Result(bool success)
    {
        Success = success;
    }

    [JsonPropertyName("success")]
    public bool Success { get; }

    [JsonPropertyName("message")]
    public string Message { get; }
}