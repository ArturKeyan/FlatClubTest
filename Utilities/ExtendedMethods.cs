using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace System
{
    public static class ExtendedMethods
    {
        public static string GetMD5(this string value)
        {
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(value));
            var builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string GetDefaultValue(this string value, string defaultValue)
        {
            return (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value)) ? value : defaultValue;
        }
    }
}

