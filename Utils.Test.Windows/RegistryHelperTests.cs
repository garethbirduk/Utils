using Gradient.Utils.Windows;
using Moq;
using Newtonsoft.Json;

namespace Gradient.Utils.Test.Windows
{
    [TestClass]
    public class RegistryHelperTests
    {
        private readonly string TestRegistryKeyPath = $"SOFTWARE\\GradientUtilsTestWindows\\{Guid.NewGuid()}";

        /// <summary>
        /// Checks if a specific subkey exists within a base registry key.
        /// </summary>
        /// <param name="baseKey">The base registry key to search in.</param>
        /// <param name="subKeyName">The name of the subkey to check.</param>
        /// <returns>True if the subkey exists, otherwise false.</returns>
        private bool SubKeyExists(Microsoft.Win32.RegistryKey baseKey, string subKeyName)
        {
            using (var subKey = baseKey.OpenSubKey(subKeyName))
            {
                return subKey != null;
            }
        }

        /// <summary>
        /// Define a simple test class for testing object serialization
        /// </summary>
        private class TestSettings
        {
            public int Age { get; set; }
            public string Name { get; set; } = "";
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                // Attempt to open and delete the GUID-based registry key directly under SOFTWARE\GradientUnitTests
                using (var baseKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\GradientUnitTests", writable: true))
                {
                    if (baseKey != null && baseKey.GetSubKeyNames().Contains(TestRegistryKeyPath.Split('\\').Last()))
                    {
                        Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(TestRegistryKeyPath, throwOnMissingSubKey: false);
                        Console.WriteLine($"Successfully cleaned up registry key: {TestRegistryKeyPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Registry key '{TestRegistryKeyPath}' does not exist. No cleanup necessary.");
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Cleanup error: Unauthorized access to the registry. {ex.Message}");
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine($"Cleanup error: Security exception occurred while accessing the registry. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup error: An unexpected error occurred during cleanup. {ex.Message}");
            }
        }

        [TestMethod]
        public void LoadObject_ShouldDeserializeObjectCorrectly()
        {
            // Arrange
            var expectedObject = new TestSettings { Name = "SampleUser", Age = 30 };
            string valueName = "TestDeserializationValue";
            string jsonString = JsonConvert.SerializeObject(expectedObject);
            RegistryHelper.SaveValue(valueName, jsonString, TestRegistryKeyPath);

            // Act
            TestSettings loadedObject = RegistryHelper.LoadObject<TestSettings>(valueName, TestRegistryKeyPath);

            // Assert
            Assert.IsNotNull(loadedObject);
            Assert.AreEqual(expectedObject.Name, loadedObject.Name);
            Assert.AreEqual(expectedObject.Age, loadedObject.Age);
        }

        [TestMethod]
        public void LoadObject_ShouldHandleCorruptedJsonGracefully()
        {
            // Arrange
            string valueName = "CorruptedJsonValue";
            string corruptedJson = "{ Name: 'John', Age: "; // Incomplete JSON
            RegistryHelper.SaveValue(valueName, corruptedJson, TestRegistryKeyPath);

            // Act
            TestSettings loadedObject = RegistryHelper.LoadObject<TestSettings>(valueName, TestRegistryKeyPath);

            // Assert
            Assert.IsNull(loadedObject); // Expected to return default value due to deserialization failure
        }

        [TestMethod]
        public void LoadObject_ShouldReturnDefaultIfValueDoesNotExist()
        {
            // Arrange
            string valueName = "NonExistentObjectValue";

            // Act
            TestSettings loadedObject = RegistryHelper.LoadObject<TestSettings>(valueName, TestRegistryKeyPath);

            // Assert
            Assert.IsNull(loadedObject);
        }

        [TestMethod]
        public void LoadValue_ShouldReturnNull_WhenRegistryKeyDoesNotExist()
        {
            // Arrange
            string nonExistentRegistryKeyPath = @"SOFTWARE\GradientUnitTests\NonExistentKey\5669FDF7-F201-47BB-8DA6-90E442EFFD95";
            string valueName = "AnyValueName";

            // Act
            string actualValue = RegistryHelper.LoadValue(valueName, nonExistentRegistryKeyPath);

            // Assert
            Assert.IsNull(actualValue);
        }

        [TestMethod]
        public void LoadValue_ShouldReturnNullIfValueDoesNotExist()
        {
            // Arrange
            string valueName = "NonExistentValue";

            // Act
            string actualValue = RegistryHelper.LoadValue(valueName, TestRegistryKeyPath);

            // Assert
            Assert.IsNull(actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(PlatformNotSupportedException))]
        public void LoadValue_ShouldThrowPlatformNotSupportedException_OnNonWindowsSystem()
        {
            // Arrange: Create a mock platform service that simulates a non-Windows environment
            var mockPlatformService = new Mock<IPlatformService>();
            mockPlatformService.Setup(service => service.IsWindows()).Returns(false);

            // Temporarily override the PlatformService with the mock service
            RegistryHelper.PlatformService = mockPlatformService.Object;

            try
            {
                // Act
                RegistryHelper.LoadValue("TestValueName", "SOFTWARE\\SomeRegistryKey");
            }
            finally
            {
                // Cleanup: Reset the PlatformService to its default value to avoid side effects in other tests
                RegistryHelper.PlatformService = new PlatformService();
            }

            // Assert: The ExpectedException attribute will handle the assertion
        }

        [TestMethod]
        public void SaveObject_ShouldSaveObjectAsJsonToRegistry()
        {
            // Arrange
            var testObject = new TestSettings { Name = "TestName", Age = 25 };
            string valueName = "TestObjectValue";

            // Act
            RegistryHelper.SaveObject(valueName, testObject, TestRegistryKeyPath);

            // Assert
            string jsonValue = RegistryHelper.LoadValue(valueName, TestRegistryKeyPath);
            var loadedObject = JsonConvert.DeserializeObject<TestSettings>(jsonValue);

            Assert.IsNotNull(loadedObject);
            Assert.AreEqual(testObject.Name, loadedObject.Name);
            Assert.AreEqual(testObject.Age, loadedObject.Age);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveObject_ShouldThrowArgumentNullException_WhenObjectIsNull()
        {
            // Arrange
            string valueName = "NullObjectValue";

            // Act
            RegistryHelper.SaveObject<object>(valueName, null, TestRegistryKeyPath);

            // Assert is handled by ExpectedException attribute
        }

        [TestMethod]
        public void SaveValue_ShouldSaveStringValueToRegistry()
        {
            // Arrange
            string valueName = "TestStringValue";
            string expectedValue = "TestValue";

            // Act
            RegistryHelper.SaveValue(valueName, expectedValue, TestRegistryKeyPath);

            // Assert
            string actualValue = RegistryHelper.LoadValue(valueName, TestRegistryKeyPath);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(PlatformNotSupportedException))]
        public void SaveValue_ShouldThrowPlatformNotSupportedException_OnNonWindowsSystem()
        {
            // Arrange: Create a mock platform service that simulates a non-Windows environment
            var mockPlatformService = new Mock<IPlatformService>();
            mockPlatformService.Setup(service => service.IsWindows()).Returns(false);

            // Temporarily override the PlatformService with the mock service
            RegistryHelper.PlatformService = mockPlatformService.Object;

            try
            {
                // Act
                RegistryHelper.SaveValue("SomeName", "TestValueName", TestRegistryKeyPath);
            }
            finally
            {
                // Cleanup: Reset the PlatformService to its default value to avoid side effects in other tests
                RegistryHelper.PlatformService = new PlatformService();
            }

            // Assert: The ExpectedException attribute will handle the assertion
        }
    }
}