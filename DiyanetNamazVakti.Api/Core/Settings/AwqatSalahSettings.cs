namespace DiyanetNamazVakti.Api.Core.Settings
{
    public class AwqatSalahSettings : IAwqatSalahSettings
    {
        public string ApiUri { get; set; }
        public int TokenLifetimeMinutes { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}