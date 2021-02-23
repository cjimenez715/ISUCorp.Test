
using System.Text.RegularExpressions;

namespace ISUCorp.Test.Api.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Delete white spaces between strings, the begin and the end
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DeleteWhiteSpaces(this string text)
        {
            return text == null ? null : Regex.Replace(text, @"\s+", " ").Trim();
        }

        /// <summary>
        /// Replace null string to Empty
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceNullByEmpty(this string text)
        {
            return text != null? text : string.Empty;
        }
    }
}
