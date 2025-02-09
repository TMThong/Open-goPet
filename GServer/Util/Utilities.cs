using Gopet.Data.user;
using System;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Gopet.Util
{
    /// <summary>
    /// Class chứa các hàm tiện ích
    /// </summary>
    public static class Utilities
    {
        private static readonly NumberFormatInfo viNumberFormat = new CultureInfo("vi").NumberFormat;
        private static readonly Random rand = new Random();
        private static readonly DateTimeFormat dateFormat = new DateTimeFormat("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// Lấy thời gian hiện tại
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
        /// <summary>
        /// Lấy thời gian hiện tại dưới dạng chuỗi
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime GetDate(string dateString)
        {
            return DateTime.Parse(dateString, dateFormat.FormatProvider);
        }
        /// <summary>
        /// Lấy thời gian hiện tại dưới dạng chuỗi
        /// </summary>
        /// <param name="miliSecondTime"></param>
        /// <returns></returns>
        public static DateTime GetDate(long miliSecondTime)
        {
            // Chuyển đổi mili giây thành ticks (1 giây = 10^7 ticks)
            long ticks = miliSecondTime * 10000;

            // Tạo đối tượng DateTime từ ticks
            DateTime o = new DateTime(ticks, DateTimeKind.Utc);

            // Trả về đối tượng DateTime
            return o;
        }
         
        public static long TimeDay(int nDays)
        {
            return CurrentTimeMillis + (nDays * 86400000L);
        }

        public static long AfterDay(int nDays)
        {
            return CurrentTimeMillis + (nDays * 86400000L);
        }

        public static long TimeHours(int nHours)
        {
            return CurrentTimeMillis + (nHours * 3600000L);
        }

        public static long TimeMinutes(int nMinutes)
        {
            return CurrentTimeMillis + (nMinutes * 60000L);
        }

        public static long TimeSeconds(long nSeconds)
        {
            return CurrentTimeMillis + (nSeconds * 1000L);
        }

        public static long TimeMillis(long nMillis)
        {
            return CurrentTimeMillis + nMillis;
        }

        public static DateTime DateDay(int nDays)
        {
            DateTime dat = DateTime.Now;
            dat = dat.AddDays(nDays);
            return dat;
        }
        /// <summary>
        /// Chuyển đổi thời gian thành chuỗi
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateString(DateTime date)
        {
            return date.ToString(dateFormat.FormatProvider);
        }

        public static DateTime DateHours(int nHours)
        {
            DateTime dat = DateTime.Now;
            dat = dat.AddHours(nHours);
            return dat;
        }

        public static DateTime DateMinutes(int nMinutes)
        {
            DateTime dat = DateTime.Now;
            dat = dat.AddMinutes(nMinutes);
            return dat;
        }

        public static DateTime DateSeconds(long nSeconds)
        {
            DateTime dat = DateTime.Now;
            dat = dat.AddSeconds(nSeconds);
            return dat;
        }
        /// <summary>
        /// Chuyển đổi thời gian thành chuỗi
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetFormatNumber(long num)
        {
            return num.ToString("N0", viNumberFormat);
        }

        public static bool CheckNumInt(string num)
        {
            return Regex.IsMatch(num, "^[0-9]+$");
        }

        public static int UnsignedByte(byte b)
        {
            return (b < 0) ? b + 256 : b;
        }

        public static string ParseString(string str, string wall)
        {
            return (str.Contains(wall)) ? str.Substring(str.IndexOf(wall) + 1) : null;
        }

        public static bool CheckString(string str, string c)
        {
            return Regex.IsMatch(str, c);
        }

        public static string StrSQL(string str)
        {
            return Regex.Replace(str, "['\"\\\\]", "\\$0");
        }

        public static int nextInt(int x1, int x2)
        {
            return rand.Next(x1, x2);
        }

        public static float NextFloat()
        {
            return rand.NextSingle();
        }

        public static float NextFloatPer()
        {
            return NextFloat() * 100;
        }

        public static int nextInt(int max)
        {
            return rand.Next(max);
        }

        public static bool NextBoolean()
        {
            return nextInt(0, 1) == 0;
        }

        public static T RandomArray<T>(T[] arr)
        {
            if (arr.Length == 1)
            {
                return arr[0];
            }
            return arr[nextInt(arr.Length)];
        }

        public static T RandomArray<T>(IEnumerable<T> arr)
        {
            if (arr.Count() == 1)
            {
                return arr.ElementAt(0);
            }
            return arr.ElementAt(nextInt(arr.Count()));
        }

        public static T RandomArray<T>(List<T> arr)
        {
            if (arr.Count == 1)
            {
                return arr[0];
            }
            return arr[nextInt(arr.Count)];
        }

        public static bool IsValidName(string s)
        {
            if (s == null)
            {
                return false;
            }
            int len = s.Length;
            for (int i = 0; i < len; i++)
            {
                if (!Char.IsLetterOrDigit(s[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static string FormatNumber(long number)
        {

            if (number == 0)
            {
                return "0";
            }

            return number.ToString("###,###,###", viNumberFormat);
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

        public static long GetValueFromPercentL(long total, float percent)
        {
            return (long) ((total / 100L) * percent);
        }

        public static string GetUID()
        {
            return Guid.NewGuid().ToString();
        }

        public static string ServerIP()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }

        public static int RandomArray(int[] optionValue)
        {
            return optionValue[nextInt(optionValue.Length)];
        }

        public static long CurrentTimeMillis
        {
            get
            {
                return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }
        }

        public static long GetTimeMillis(this DateTime dateTime)
        {
            return dateTime.Ticks / TimeSpan.TicksPerMillisecond;
        }


        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long CurrentTimeMillisJava
        {
            get
            {
                return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
            }
        }

        public static string Format(string value, params object[] objects)
        {
            int num = 0;
            string text = string.Empty + value;
            while (text.Contains("%s"))
            {
                int indexOf = text.IndexOf("%s");
                text = text.Remove(indexOf, 2);
                text = text.Insert(indexOf, "{" + num + "}");
                num++;
            }
            return string.Format(text, objects);
        }

        public static void printStackTrace(this Exception e)
        {
            GopetManager.ServerMonitor.LogError(e.ToString());
        }

        public static int round(float value)
        {
            return (int)Math.Round(value);
        }

        public static bool ContainsKeyZ<TKey, TValue>(this IDictionary<TKey, TValue> keyValuePairs, params TKey[] keys)
        {

            foreach (var item in keys)
            {
                if (!keyValuePairs.ContainsKey(item)) return false;
            }

            return true;
        }

        public static bool ContainsZ<T>(this IEnumerable<T> collection, params T[] values)
        {

            foreach (var item in values)
            {
                if (!collection.Contains(item)) return false;
            }

            return true;
        }


        public static T BinarySearch<T>(this IEnumerable<IBinaryObject<T>> binaryObjects, int Id)
        {
            int left = 0;
            int right = binaryObjects.Count() - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                IBinaryObject<T> midItem = binaryObjects.ElementAt(mid);
                if (midItem.GetId() == Id)
                    return midItem.Instance;
                if (midItem.GetId() < Id)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return default(T);
        }

        public static void BinaryObjectAdd<T>(this IEnumerable<IBinaryObject<T>> binaryObjects, IBinaryObject<T> binaryObject)
        {
            bool flagId = false;
            while (true)
            {
                if (binaryObject.GetId() > 0 && !flagId)
                {
                    flagId = true;
                }
                else
                {
                    binaryObject.SetId(Utilities.nextInt(10, int.MaxValue - 2));
                }
                bool flag = true;
                foreach (var item1 in binaryObjects)
                {
                    if (item1 != binaryObject)
                    {
                        if (item1.GetId() == binaryObject.GetId())
                        {
                            flag = false;
                        }
                    }
                }
                if (flag)
                {
                    break;
                }
            }
        }

        public static string Join<T>(this IEnumerable<T> values, string char_)
        {
            string joinText = string.Empty;
            if (!values.Any())
            {
                return string.Empty;
            }
            for (int i = 0; i < values.Count(); i++)
            {
                if (i + 1 >= values.Count())
                    joinText += values.ElementAt(i).ToString();
                else
                    joinText += values.ElementAt(i).ToString() + char_;
            }
            return joinText;
        }
    }
}