namespace Gradient.Utils
{
    /// <summary>
    /// Static helper for getting attributes
    /// </summary>
    public static class AttributeHelper
    {
        /// <summary>
        /// Returns the first attribute of an Enum value, or a default value if the Enum value containts no such attributes.
        /// </summary>
        /// <typeparam name="T">The type of Attribute to return.</typeparam>
        /// <param name="myEnum">The enum value to find Attributes on, e.g. MyColors.Red</param>
        /// <returns>The first attribute, or default.</returns>
        public static T? EnumAttributeFirstOrDefault<T>(this Enum myEnum)
            where T : Attribute
        {
            return myEnum.EnumAttributes<T>().FirstOrDefault();
        }

        /// <summary>
        /// Returns the only attribute of an Enum value, or a default value if the Enum value containts no such attributes.
        /// </summary>
        /// <typeparam name="T">The type of Attribute to return.</typeparam>
        /// <param name="myEnum">The enum value to find Attributes on, e.g. MyColors.Red</param>
        /// <returns>The single attribute, or default.</returns>
        public static T? EnumAttributeSingleOrDefault<T>(this Enum myEnum)
            where T : Attribute
        {
            return myEnum.EnumAttributes<T>().SingleOrDefault();
        }

        /// <summary>
        /// Returns the attributes of an Enum value.
        /// </summary>
        /// <typeparam name="T">The type of Attribute to return in an IEnumerable.</typeparam>
        /// <param name="myEnum">The enum value to find Attributes on, e.g. MyColors.Red</param>
        /// <returns>The attributes in an IEnumerable.</returns>
        public static IEnumerable<T?> EnumAttributes<T>(this Enum myEnum)
            where T : Attribute
        {
            return myEnum.GetType()
                .GetMember(myEnum.ToString())
                .Where(x => true)
                .Single() // This cannot be null as myEnum has to exist within the members of myEnum.GetType() by definition
                .GetCustomAttributes(typeof(T), false) as T[] ?? Array.Empty<T?>();
        }
    }
}
