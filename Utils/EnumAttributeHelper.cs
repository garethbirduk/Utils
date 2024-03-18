using System;

namespace Gradient.Utils
{
    /// <summary>
    /// Static helper for getting attributes on enums
    /// </summary>
    public static class EnumAttributeHelper
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
        /// Returns the attributes of an Enum value.
        /// </summary>
        /// <typeparam name="T">The type of Attribute to return in an IEnumerable.</typeparam>
        /// <param name="myEnum">The enum value to find Attributes on, e.g. MyColors.Red</param>
        /// <returns>The attributes in an IEnumerable.</returns>
        public static IEnumerable<T> EnumAttributes<T>(this Enum myEnum)
            where T : Attribute
        {
            var attribute = myEnum
                .GetType()
                .GetMember(myEnum.ToString())
                .SingleOrDefault();

            if (attribute == null)
                return Enumerable.Empty<T>();

            return attribute
                .GetCustomAttributes(typeof(T), false)
                .OfType<T>();
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
    }
}