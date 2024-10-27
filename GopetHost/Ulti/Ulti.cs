using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GopetHost.Ulti
{
    public static class Ulti
    {
        public static readonly Random Random = new Random();
        public const string RAN_CHARS = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        public static readonly Regex UserPassRegex = new Regex(@"^[a-zA-Z0-9]{5,20}$");
        public static readonly Regex CharNameRegex = new Regex(@"^[a-z0-9]{5,10}$");
        public static readonly Regex EmailRegex = new Regex(@"^[a-z0-9@]{5,200}$");
        public static string RandomChars(int length)
        {
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                str += RAN_CHARS.ElementAt(Next(RAN_CHARS.Length));
            }
            return str;
        }

        public static int Next(int Max) => Random.Next(Max);
        public static int Next(int Min, int Max) => Min == Max ? Max : Random.Next(Min, Max);
        public static T Next<T>(T[] values) => values[Next(values.Length)];
        public static float NextPercent() => Random.NextSingle() * 100;
        public static long CurrentTimeMillis
        {
            get
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
            }
        }

        public static long GetTimeMillis(DateTime dateTimeFormat)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (dateTimeFormat.Ticks - dateTime.Ticks) / 10000L;
        }

        public static void Clear<TKey, TValue>(this IDictionary<TKey, TValue> keyValuePairs, Func<KeyValuePair<TKey, TValue>, bool> func)
        {
            foreach (var item in keyValuePairs.Keys.ToArray())
            {
                if (keyValuePairs.TryGetValue(item, out TValue val))
                {
                    if (func(new KeyValuePair<TKey, TValue>(item, val)))
                    {
                        keyValuePairs.Remove(item);
                    }
                }
            }
        }

        public static T[] Cut<T>(this T[] values, int max)
        {
            if (max > 0)
            {
                if (max > values.Length)
                {
                    return values;
                }
                T[] result = new T[max];
                for (global::System.Int32 i = 0; i < max; i++)
                {
                    result[i] = values[i];
                }
                return result;
            }
            return new T[0];
        }

        public static float Percent(float total, float value)
        {
            return value / total * 100;
        }

        public static float Percent(long total, long value)
        {
            return Percent((float)total, (float)value);
        }

        public static float GetValueFromPercent(float total, float percent)
        {
            return total / 100 * percent;
        }

        public static int PercentInt(float total, float value)
        {
            return (int)Math.Round(Percent(total, value));
        }

        public static int PercentInt(long total, long value)
        {
            return (int)Math.Round(Percent(total, value));
        }

        public static int GetValueFromPercentInt(float total, float percent)
        {
            return (int)Math.Round(GetValueFromPercent(total, percent));
        }

        public static int[] FromTo(int Min, int Max)
        {
            int[] result = new int[Max - Min + 1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Min + i;
            }

            return result;
        }

        public static int NextEqual(int min, int max) => Next(min - 1, max + 1);

        public static IEnumerable<System.Enum> GetFlags(this System.Enum input)
        {
            foreach (System.Enum value in System.Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }

        public static string ComputeSha256Hash(string Text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("TAE" + String.Join(string.Empty, Text.Reverse()) + "TAE"));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifySha256Hash(string Hash, string Text)
        {
            string hashOfInput = ComputeSha256Hash(Text);
            return hashOfInput == Hash;
        }

        public static string FormatNum(int num)
        {
            if (num == 0)
            {
                return num.ToString();
            }
            return num.ToString("###,###,###,###").Replace(',', '.');
        }
    }
}
