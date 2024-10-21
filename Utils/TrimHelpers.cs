namespace Gradient.Utils
{
    public static class TrimHelpers
    {
        /// <summary>
        /// Trims the specified substring from both the start and the end of the string.
        /// Continues trimming as long as the string starts or ends with the specified substring.
        /// </summary>
        /// <param name="source">The source string to trim.</param>
        /// <param name="trimString">The substring to remove from both the start and the end of the string.</param>
        /// <returns>A string with the specified substring removed from both the start and end.</returns>
        public static string Trim(this string source, string trimString)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(trimString))
                return source;

            return source.TrimStart(trimString).TrimEnd(trimString);
        }

        /// <summary>
        /// Trims the specified substring from the end of the string, if it exists.
        /// Continues trimming as long as the string ends with the specified substring.
        /// </summary>
        /// <param name="source">The source string to trim.</param>
        /// <param name="trimString">The substring to remove from the end of the string.</param>
        /// <returns>A string with the specified substring removed from the end.</returns>
        public static string TrimEnd(this string source, string trimString)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(trimString))
                return source;

            while (source.EndsWith(trimString))
            {
                source = source.Substring(0, source.Length - trimString.Length);
            }

            return source;
        }

        /// <summary>
        /// Trims the specified substring from the start of the string, if it exists.
        /// Continues trimming as long as the string starts with the specified substring.
        /// </summary>
        /// <param name="source">The source string to trim.</param>
        /// <param name="trimString">The substring to remove from the start of the string.</param>
        /// <returns>A string with the specified substring removed from the start.</returns>
        public static string TrimStart(this string source, string trimString)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(trimString))
                return source;

            while (source.StartsWith(trimString))
            {
                source = source.Substring(trimString.Length);
            }

            return source;
        }
    }
}