using Microsoft.Win32;
using Newtonsoft.Json;

namespace Gradient.Utils.Windows
{
    public interface IPlatformService
    {
        bool IsWindows();
    }

    public static class RegistryHelper
    {
        // Static property to access the platform service
        public static IPlatformService PlatformService { get; set; } = new PlatformService();

        /// <summary>
        /// Loads an object of type <T> from a JSON string in the registry.
        /// </summary>
        /// <typeparam name="T">The type of the object to load.</typeparam>
        /// <param name="valueName">The name of the value to load.</param>
        /// <param name="registryKeyPath">The path to the registry key.</param>
        /// <returns>The deserialized object of type <T>, or default(T) if not found or if deserialization fails.</returns>
        public static T LoadObject<T>(string valueName, string registryKeyPath)
        {
            string jsonString = LoadValue(valueName, registryKeyPath);

            if (string.IsNullOrEmpty(jsonString))
            {
                return default; // Return default value if the registry value is not found or empty
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Failed to deserialize JSON value for '{valueName}': {ex.Message}");
                return default; // Return default value if deserialization fails
            }
        }

        /// <summary>
        /// Loads a string value from the registry.
        /// </summary>
        public static string LoadValue(string valueName, string registryKeyPath)
        {
            if (!PlatformService.IsWindows())
            {
                throw new PlatformNotSupportedException("This method is only supported on Windows.");
            }

            using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryKeyPath))
            {
                if (key != null)
                {
                    return key.GetValue(valueName) as string;
                }
            }
            return null; // Value not found
        }

        /// <summary>
        /// Saves an object as a JSON string to the registry.
        /// </summary>
        /// <typeparam name="T">The type of the object to save.</typeparam>
        /// <param name="valueName">The name of the value to save.</param>
        /// <param name="objectToSave">The object to be saved in the registry.</param>
        /// <param name="registryKeyPath">The path to the registry key.</param>
        public static void SaveObject<T>(string valueName, T objectToSave, string registryKeyPath)
        {
            if (objectToSave == null)
            {
                throw new ArgumentNullException(nameof(objectToSave), "Object to save cannot be null.");
            }

            string jsonString = JsonConvert.SerializeObject(objectToSave);
            SaveValue(valueName, jsonString, registryKeyPath);
        }

        /// <summary>
        /// Saves a string value to the registry.
        /// </summary>
        public static void SaveValue(string valueName, string value, string registryKeyPath)
        {
            if (!PlatformService.IsWindows())
            {
                throw new PlatformNotSupportedException("This method is only supported on Windows.");
            }

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKeyPath))
            {
                if (key != null)
                {
                    key.SetValue(valueName, value);
                }
            }
        }
    }

    public class PlatformService : IPlatformService
    {
        public bool IsWindows()
        {
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
        }
    }
}