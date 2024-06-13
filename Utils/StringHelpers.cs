using PostSharp.Patterns.Contracts;

namespace Gradient.Utils
{
    public static class StringHelpers
    {
        /// <summary>
        /// Generate a random name based on a Guid
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomName(string prefix = "", string suffix = "", [LessThan(37)] int length = 8)
        {
            return $"{prefix}{string.Join("", Guid.NewGuid().ToString().Take(length))}{suffix}";
        }
    }
}