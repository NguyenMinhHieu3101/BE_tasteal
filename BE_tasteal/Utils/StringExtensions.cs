using AutoMapper.Execution;
using System.Globalization;
using System.Text;

namespace BE_tasteal.Utils
{
    public static class StringExtensions
    {
        public static string RemoveDiacritics(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var normalizedString = input.Normalize(NormalizationForm.FormKD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }
    }

}

