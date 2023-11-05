using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BE_tasteal.API.Middleware
{
    public class ValidateTimeSpanString : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string stringValue = value.ToString();

                // Gọi hàm bool để kiểm tra chuỗi
                if (IsDateTimeFormatValid(stringValue))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }

        static bool IsDateTimeFormatValid(string input)
        {
            DateTime dateTime;
            return DateTime.TryParseExact(input, "yyyy-MM-ddTHH:mm:ss.fffZ", null, DateTimeStyles.AssumeUniversal, out dateTime);
        }
    }
}
