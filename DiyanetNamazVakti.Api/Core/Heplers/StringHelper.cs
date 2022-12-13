namespace DiyanetNamazVakti.Api.Core.Heplers;

public static class StringHelper
{
    public static string ClearForHtml(this string str)
    {
        return Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();
    }


    /// <summary>
    /// Enum olarak verilen parametrenin Description deðerini döndürür.
    /// </summary>
    /// <param name="e">Enum</param>
    /// <returns>string</returns>
    public static string GetEnumDescription(this Enum e)
    {
        return e.GetType().GetMember(e.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description;
    }

    /// <summary>
    /// string olarak verilen parametrenin deðerini SEO'ya (Arama Motoru Optimazsyonu) uygun olarak dönüþtürerek geri döndürür.
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string ToStringForSeo(this string inputString)
    {
        inputString = inputString.Replace("Ç", "c");
        inputString = inputString.Replace("ç", "c");
        inputString = inputString.Replace("Ð", "g");
        inputString = inputString.Replace("ð", "g");
        inputString = inputString.Replace("I", "i");
        inputString = inputString.Replace("ý", "i");
        inputString = inputString.Replace("Ý", "i");
        inputString = inputString.Replace("i", "i");
        inputString = inputString.Replace("Ö", "o");
        inputString = inputString.Replace("ö", "o");
        inputString = inputString.Replace("Þ", "s");
        inputString = inputString.Replace("þ", "s");
        inputString = inputString.Replace("Ü", "u");
        inputString = inputString.Replace("ü", "u");
        inputString = inputString.Trim().ToLower();
        inputString = Regex.Replace(inputString, @"\s+", "-");
        inputString = Regex.Replace(inputString, @"[^A-Za-z0-9_-]", "");
        return inputString;
    }

    /// <summary>
    /// string olarak verilen parametrenin deðerindeki kelimelerin ilk harflerini büyük harfe çevirir.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="useOriginal">false olmasý durumunda "Ve, Ýle" baðlaçlarý küçük yapýlýr.</param>
    /// <returns></returns>
    public static string ToTitleCase(this string str, bool useOriginal = false)
    {
        var result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());

        if (!useOriginal)
        {
            result = result.Replace(" Ve ", " ve ");
            result = result.Replace(" Ýle ", " ile ");
            return result;
        }
        return result;
    }

    public static string ListToString(this List<string> list, string delimeter)
    {
        return string.Join(delimeter, list.ToArray());
    }

    public static string TemplateParser(this string templateText, string regExString, string value)
    {
        var regExToken = new Regex(regExString, RegexOptions.IgnoreCase);
        return regExToken.Replace(templateText, value);
    }
}