namespace DiyanetNamazVakti.Api.Core.Constants
{
    public static class JsonConstants
    {
        public static JsonSerializerOptions SerializerOptions => new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}