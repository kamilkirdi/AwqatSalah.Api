namespace DiyanetNamazVakti.Api.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Geçersiz işlemler için istisna sınıfı
/// </summary>
public class InvalidTransactionException : BaseApplicationException
{
    public InvalidTransactionException(string message) : base(message) { }
}