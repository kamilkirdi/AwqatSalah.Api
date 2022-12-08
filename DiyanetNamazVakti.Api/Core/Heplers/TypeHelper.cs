namespace DiyanetNamazVakti.Api.Core.Heplers
{
    public static class TypeHelper
    {

        public static byte ToByte(this object obj, byte defaultValue = default) => obj == null ? defaultValue : !byte.TryParse(obj.ToString(), out var result) ? defaultValue : result;

        public static short ToShort(this object obj, short defaultValue = default) => obj == null ? defaultValue : !short.TryParse(obj.ToString(), out var result) ? defaultValue : result;


        public static int ToInt(this object obj, int defaultValue = default) => obj == null ? defaultValue : !int.TryParse(obj.ToString(), out var result) ? defaultValue : result;


        public static double ToDouble(this object obj, double defaultValue = default) => obj == null ? defaultValue : !double.TryParse(obj.ToString(), out var result) ? defaultValue : result;


        public static decimal ToDecimal(this object obj, decimal defaultValue = default) => obj == null ? defaultValue : !decimal.TryParse(obj.ToString(), out var result) ? defaultValue : result;


        public static DateTime ToDateTime(this object obj, DateTime defaultValue = default) => obj == null || string.IsNullOrEmpty(obj.ToString())
                ? defaultValue
                : !DateTime.TryParse(obj.ToString(), out var result) ? defaultValue : result;


        public static bool IsNumeric(this string input) => int.TryParse(input, out var result);

        public static T DeserializeFromString<T>(this string data) => JsonSerializer.Deserialize<T>(data, JsonConstants.SerializerOptions)!;

        public static string SerializeToString<T>(this T data) => JsonSerializer.Serialize(data, JsonConstants.SerializerOptions);

        public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);

        public static bool ToBoolean(this string str)
        {

            string[] trueStrings = { "1", "Y", "y", "yes", "true", "Evet", "E", "evet", "on" };

            string[] falseStrings = { "0", "N", "n", "no", "false", "Hayýr", "Hayir", "H", "hayýr", "hayir", "off" };

            if (trueStrings.Contains(str, StringComparer.OrdinalIgnoreCase))
            {
                return true;
            }

            if (falseStrings.Contains(str, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            throw new InvalidCastException("Yalnýzca þu ifadeler dönüþtürülür: " + string.Join(", ", trueStrings) + " ve " + string.Join(", ", falseStrings));

        }

        public static byte[] ToByteArray(this object obj) => JsonSerializer.SerializeToUtf8Bytes(obj);

        public static T FromByteArray<T>(this byte[] byteArray) => JsonSerializer.Deserialize<T>(byteArray)!;

    }
}