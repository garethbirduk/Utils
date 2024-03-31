#pragma warning disable CS8603 // Possible null reference return.

using System;
using Utils.Attributes;

namespace Utils
{
    public static class EnumHelper
    {
        /// <summary>
        /// Returns the Aliases of a given enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<string> Aliases(this Enum value)
        {
            var attribute = value.AttributeFirstOrDefault<AliasAttribute>();
            return attribute == null ? [] : attribute.List;
        }

        /// <summary>
        /// Returns the first alias of an enum value, or default.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AliasFirstOrDefault(this Enum value)
        {
            var attribute = value.AttributeFirstOrDefault<AliasAttribute>();
            return attribute?.List.FirstOrDefault();
        }

        /// <summary>
        /// Returns the only alias of an enum value, or default.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AliasSingleOrDefault(this Enum value)
        {
            var attribute = value.AttributeFirstOrDefault<AliasAttribute>();
            return attribute?.List.SingleOrDefault();
        }

        /// <summary>
        /// Get the first attribute of a given type from an enum member, or null if not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T AttributeFirstOrDefault<T>(this Enum value)
            where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
              ? (T)attributes[0]
              : null;
        }

        /// <summary>
        /// Get the enum value of a enum type with an AliasAttribute with the given alias, or default.
        /// Note that the enum value itself will be excluded if it is not included in the AliasAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T StringToEnumAliasOrDefault<T>(string value) where T : Enum
        {
            foreach (T v in Enum.GetValues(typeof(T)))
            {
                var aliases = Aliases(v).Where(x => StringComparer.OrdinalIgnoreCase.Equals(x, value));
                if (aliases.Any())
                    return v;
            }
            return default;
        }

        /// <summary>
        /// Get the enum value of a enum type with a given name, or default. Optionally allow use of the AliasAttribute
        /// for other matches.
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The string value of the enum to be found</param>
        /// <param name="allowAlias">Find the string value in an AliasAttribute</param>
        /// <returns></returns>
        public static T StringToEnumOrDefault<T>(string value, bool allowAlias = false)
            where T : Enum
        {
            if (Enum.TryParse(typeof(T), value, true, out var e))
                return (T)e;
            if (allowAlias)
            {
                return StringToEnumAliasOrDefault<T>(value);
            }
            return default;
        }
    }
}

#pragma warning restore CS8603 // Possible null reference return.