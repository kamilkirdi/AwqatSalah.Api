using DiyanetNamazVakti.Api.Core.Globalization;

namespace DiyanetNamazVakti.Api.Service.Models
{
    public class TokenModel
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
namespace DiyanetNamazVakti.Api.Service.Models
{
    public class TokenModelV2
    {
        public TokenModel Data { get; set; }
    }
}