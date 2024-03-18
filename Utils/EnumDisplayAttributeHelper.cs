using System.ComponentModel.DataAnnotations;

namespace Gradient.Utils
{
    /// <summary>
    /// Static helper for getting properties from a DisplayAttribute on enums
    /// </summary>
    public static class EnumDisplayAttributeHelper
    {
        /// <summary>
        /// Return a description from possible DisplayAttribute or default.
        /// </summary>
        /// <param name="myEnumVariable"></param>
        /// <returns></returns>
        public static string DisplayDescriptionOrDefault(this Enum myEnumVariable)
        {
            return myEnumVariable.EnumAttributeFirstOrDefault<DisplayAttribute>()?.Description ?? "";
        }

        /// <summary>
        /// Return a group name from possible DisplayAttribute or default.
        /// </summary>
        /// <param name="myEnumVariable"></param>
        /// <returns></returns>
        public static string DisplayGroupNameOrDefault(this Enum myEnumVariable)
        {
            return myEnumVariable.EnumAttributeFirstOrDefault<DisplayAttribute>()?.GroupName ?? "";
        }

        /// <summary>
        /// Return a name from possible DisplayAttribute or default.
        /// </summary>
        /// <param name="myEnumVariable"></param>
        /// <returns></returns>
        public static string DisplayNameOrDefault(this Enum myEnumVariable)
        {
            return myEnumVariable.EnumAttributeFirstOrDefault<DisplayAttribute>()?.Name ?? "";
        }

        /// <summary>
        /// Return a prompt from possible DisplayAttribute or default.
        /// </summary>
        /// <param name="myEnumVariable"></param>
        /// <returns></returns>
        public static string DisplayPromptOrDefault(this Enum myEnumVariable)
        {
            return myEnumVariable.EnumAttributeFirstOrDefault<DisplayAttribute>()?.Prompt ?? "";
        }

        /// <summary>
        /// Return a short name from possible DisplayAttribute or default.
        /// </summary>
        /// <param name="myEnumVariable"></param>
        /// <returns></returns>
        public static string DisplayShortNameOrDefault(this Enum myEnumVariable)
        {
            return myEnumVariable.EnumAttributeFirstOrDefault<DisplayAttribute>()?.ShortName ?? "";
        }
    }
}