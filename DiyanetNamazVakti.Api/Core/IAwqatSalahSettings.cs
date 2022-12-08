namespace DiyanetNamazVakti.Api.Core
{
    public interface IAwqatSalahSettings
    {
        public string ApiUri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}