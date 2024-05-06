namespace Gradient.Utils.Attributes
{
    /// <summary>
    /// Used to provide a string list of alias names for assisting in interrogation through Reflection.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="list">The alias(es)</param>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class AliasAttribute(params string[] list) : Attribute
    {
        /// <summary>
        /// The list of aliases.
        /// </summary>
        public List<string> List { get; } = list.ToList();
    }
}