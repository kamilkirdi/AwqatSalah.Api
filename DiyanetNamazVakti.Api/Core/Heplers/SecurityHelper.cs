using System.Text;

namespace DiyanetNamazVakti.Api.Core.Heplers
{
    /// <summary>
    /// Þifreleme iþlemleri için yardýmcý sýnýf
    /// </summary>
    public static class SecurityHelper
    {
        static readonly string key = "6c879798-ee9a-4c97-9eec-6fe5b1b5204b";
        static readonly byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };


        /// <summary>
        /// Düz metni kriptolu hale getirir.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(this string plainText)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                var rfc = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = rfc.GetBytes(32);
                encryptor.IV = rfc.GetBytes(16);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                plainText = Convert.ToBase64String(ms.ToArray());
            }
            return plainText;
        }

        /// <summary>
        /// Kriptolu bir ifadeyi düz metin haline getirir.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static string Decrypt(this string encryptedText)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes encryptor = Aes.Create())
            {
                var rfc = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = rfc.GetBytes(32);
                encryptor.IV = rfc.GetBytes(16);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                encryptedText = Encoding.UTF8.GetString(ms.ToArray());
            }
            return encryptedText;

        }

        /// <summary>
        /// HMACSHA256 algoritmasý kullanarak özet oluþturur.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToHmacSha256(string input, string key)
        {
            var byteKey = Encoding.UTF8.GetBytes(key);
            using var hash = new HMACSHA256(byteKey);
            var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Kurallara uygun þifre olup olmadýðýný kontrol eder.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="minLength"></param>
        /// <param name="numUpper"></param>
        /// <param name="numLower"></param>
        /// <param name="numNumbers"></param>
        /// <param name="numSpecial"></param>
        /// <returns></returns>
        public static bool ValidatePassword(this string password, int minLength = 8, int numUpper = 1, int numLower = 1, int numNumbers = 1, int numSpecial = 1)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            var upper = new Regex("[A-Z]");
            var lower = new Regex("[a-z]");
            var number = new Regex("[0-9]");
            var special = new Regex("[^a-zA-Z0-9]");

            if (password.Length < minLength)
            {
                return false;
            }

            if (upper.Matches(password).Count < numUpper)
            {
                return false;
            }
            if (lower.Matches(password).Count < numLower)
            {
                return false;
            }
            if (number.Matches(password).Count < numNumbers)
            {
                return false;
            }
            return special.Matches(password).Count >= numSpecial;
        }

        /// <summary>
        /// SHA512 algoritmasý kullanarak özet oluþturur. 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSha512(this string str)
        {
            using var hashTool = SHA512.Create();
            var encryptedBytes = hashTool.ComputeHash(Encoding.UTF8.GetBytes(str));
            var stringBuilder = new StringBuilder();
            foreach (var t in encryptedBytes)
            {
                stringBuilder.Append(t.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Kullanýcýlar için rastgele þifre oluþturur.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>

        public static string CreatePassword(int length)
        {
            //const string upperCaseChars = "ABCDEFGHJKLMNPQRSTWXYZ";
            //const string lowerCaseChars = "abcdefgijkmnopqrstwxyz";
            const string numericChars = "0123456789";
            //   const string specialChars = "%#+-!@?";
            var charGroups = new[]
            {
            //upperCaseChars.ToCharArray(),
            //lowerCaseChars.ToCharArray(),
            numericChars.ToCharArray(),
        //    specialChars.ToCharArray()
        };
            var charsLeftInGroup = new int[charGroups.Length];
            for (var i = 0; i < charsLeftInGroup.Length; i++)
            {
                charsLeftInGroup[i] = charGroups[i].Length;
            }
            var leftGroupsOrder = new int[charGroups.Length];
            for (var i = 0; i < leftGroupsOrder.Length; i++)
            {
                leftGroupsOrder[i] = i;
            }
            var randomBytes = new byte[4];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            var seed = BitConverter.ToInt32(randomBytes, 0);
            var random = new Random(seed);
            var password = new char[length];
            var lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
            for (var i = 0; i < password.Length; i++)
            {
                var nextLeftGroupsOrderIdx = lastLeftGroupsOrderIdx == 0 ? 0 : random.Next(0, lastLeftGroupsOrderIdx);
                var nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];
                var lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;
                var nextCharIdx = lastCharIdx == 0 ? 0 : random.Next(0, lastCharIdx + 1);
                password[i] = charGroups[nextGroupIdx][nextCharIdx];
                if (lastCharIdx == 0)
                {
                    charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
                }
                else
                {
                    if (lastCharIdx != nextCharIdx)
                    {
                        var temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] = charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    charsLeftInGroup[nextGroupIdx]--;
                }
                if (lastLeftGroupsOrderIdx == 0)
                {
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                }
                else
                {
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        var temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    lastLeftGroupsOrderIdx--;
                }
            }
            return new string(password);
        }

        /// <summary>
        /// Rastgele ID oluþturur.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>

        public static string CreateId(int length)
        {
            var sb = new StringBuilder();

            var letters = "ABCDEFGHIJKLMNOPRSTUVYZQXW".ToCharArray().Select(c => c.ToString()).ToArray();

            var numbers = DateTime.Now.Ticks.ToString().ToCharArray().Select(c => c.ToString()).ToArray();

            for (var i = 1; i <= length; i++)
            {
                var random1 = new Random();
                var int1 = random1.Next(letters.Length);
                var str1 = letters[int1];
                sb.Append(str1);

                var random2 = new Random();
                var int2 = random2.Next(numbers.Length);
                var str2 = numbers[int2];
                sb.Append(str2);

                var random3 = new Random();
                var int3 = random3.Next(letters.Length);
                var str3 = letters[int3];
                sb.Append(str3);

                var random4 = new Random();
                var int4 = random4.Next(numbers.Length);
                var str4 = numbers[int4];
                sb.Append(str4);

                if (i != length)
                {
                    sb.Append("-");
                }
            }

            return sb.ToString();
        }

    }
}