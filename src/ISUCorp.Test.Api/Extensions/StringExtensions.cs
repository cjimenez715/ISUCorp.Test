
using System.Text.RegularExpressions;

namespace ISUCorp.Test.Api.Extensions
{
    //Strings Extensions
    public static class StringExtensions
    {
        public static string DeleteWhiteSpaces(this string text)
        {
            return text == null ? null : Regex.Replace(text, @"\s+", " ").Trim();
        }

        public static string ReplaceNullByEmpty(this string text)
        {
            return text != null ? text : string.Empty;
        }
    }
}
