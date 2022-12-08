namespace DiyanetNamazVakti.Api.Service.Models;

public class TokenWithExpireModel: TokenModel
{
    public DateTime ExpireTime { get; set; }
}