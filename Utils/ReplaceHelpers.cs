namespace Gradient.Utils
{
    public static class ReplaceHelpers
    {
        /// <summary>
        /// Replaces the first occurrence of a specified substring within the input string.
        /// </summary>
        /// <param name="input">The input string where the replacement will be made.</param>
        /// <param name="search">The substring to search for within the input string.</param>
        /// <param name="replace">The substring to replace the first occurrence of the search substring.</param>
        /// <returns>A new string with the first occurrence of the search substring replaced.</returns>
        public static string ReplaceFirst(this string input, string search, string replace)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(search))
                return input;

            int pos = input.IndexOf(search);
            if (pos < 0)
                return input;

            return input.Substring(0, pos) + replace + input.Substring(pos + search.Length);
        }

        /// <summary>
        /// Replaces the first occurrence of a specified substring up to a maximum number of times within the input string.
        /// </summary>
        /// <param name="input">The input string where the replacement will be made.</param>
        /// <param name="search">The substring to search for within the input string.</param>
        /// <param name="replace">The substring to replace the first occurrences of the search substring.</param>
        /// <param name="maxInstances">The maximum number of instances to replace.</param>
        /// <returns>A new string with the first occurrence(s) of the search substring replaced up to the maximum number of instances.</returns>
        public static string ReplaceFirst(this string input, string search, string replace, int maxInstances)
        {
            for (int i = 0; i < maxInstances; i++)
            {
                string newInput = input.ReplaceFirst(search, replace);
                if (newInput == input)
                    break; // No more replacements
                input = newInput;
            }
            return input;
        }

        /// <summary>
        /// Replaces the last occurrence of a specified substring within the input string.
        /// </summary>
        /// <param name="input">The input string where the replacement will be made.</param>
        /// <param name="search">The substring to search for within the input string.</param>
        /// <param name="replace">The substring to replace the last occurrence of the search substring.</param>
        /// <returns>A new string with the last occurrence of the search substring replaced.</returns>
        public static string ReplaceLast(this string input, string search, string replace)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(search))
                return input;

            int pos = input.LastIndexOf(search);
            if (pos < 0)
                return input;

            return input.Substring(0, pos) + replace + input.Substring(pos + search.Length);
        }

        /// <summary>
        /// Replaces the last occurrence of a specified substring up to a maximum number of times within the input string.
        /// </summary>
        /// <param name="input">The input string where the replacement will be made.</param>
        /// <param name="search">The substring to search for within the input string.</param>
        /// <param name="replace">The substring to replace the last occurrences of the search substring.</param>
        /// <param name="maxInstances">The maximum number of instances to replace.</param>
        /// <returns>A new string with the last occurrence(s) of the search substring replaced up to the maximum number of instances.</returns>
        public static string ReplaceLast(this string input, string search, string replace, int maxInstances)
        {
            for (int i = 0; i < maxInstances; i++)
            {
                string newInput = input.ReplaceLast(search, replace);
                if (newInput == input)
                    break; // No more replacements
                input = newInput;
            }
            return input;
        }
    }
}