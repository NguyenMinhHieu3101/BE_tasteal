using System.Text.RegularExpressions;

namespace BE_tasteal.Utils
{
    public static class DateConvert
    {
        public static TimeSpan ParseTimeSpan(string input)
        {
            Regex regex = new Regex(@"(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?");
            Match match = regex.Match(input);

            int hours = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
            int minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
            int seconds = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;

            TimeSpan timeSpan = new TimeSpan(hours, minutes, seconds);
            return timeSpan;
        }

        public static DateTime ConvertToDateTime(string inputDateTime)
        {
            // Chuỗi ngày giờ đầu vào
            string input = "2023-12-29T08:45:30.123Z"; // Ví dụ chuỗi đầu vào

            // Định dạng của chuỗi đầu vào
            string format = "yyyy-MM-ddTHH:mm:ss.fffZ";

            // Chuyển đổi từ chuỗi ngày giờ sang kiểu DateTime
            if (DateTime.TryParseExact(input, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Không thể chuyển đổi chuỗi ngày giờ.");
            }
        }
    }
}
