using Microsoft.VisualBasic;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab6.Utils
{
    public class DateUtils
    {
        private static readonly string[] DateFormats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "yyyyMMdd",
            "dd-MM-yyyy",
            "MM-dd-yyyy"
        };

        public static DateTime ParseDate(string dateValue)
        {
            if (DateTime.TryParseExact(dateValue, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine($"Date value : {date}");
                return date;
            }
            else
            {
                throw new FormatException($"Invalid date format: {dateValue}");
            }
        }

        public static bool areDatesEqual(DateTime? nullableDate, DateTime ordinaryDate)
        {
            if (nullableDate.Equals(ordinaryDate)) return true;

            if (nullableDate.HasValue)
            {
                return
                    nullableDate.Value.Year == ordinaryDate.Year &&
                    nullableDate.Value.Month == ordinaryDate.Month &&
                    nullableDate.Value.Day == ordinaryDate.Day;
            }

            return false;
        }
    }
}
