namespace DiyanetNamazVakti.Api.Core;

public interface IDataResult<out T> : IResult
{
    T Data { get; }
}