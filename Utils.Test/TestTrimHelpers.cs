namespace Gradient.Utils.Test
{
    /// <summary>
    /// Test class for StringHelpers
    /// </summary>
    [TestClass]
    public class TestTrimHelpers
    {
        [DataTestMethod]
        [DataRow("testHelloWorldtest", "test", "HelloWorld")] // 'test' at both start and end
        [DataRow("testtestHelloWorldtesttest", "test", "HelloWorld")] // Double 'test' at both start and end
        [DataRow("HelloWorld", "test", "HelloWorld")] // No 'test' at start or end
        [DataRow("test", "test", "")] // Only 'test' in the string
        [DataRow("", "test", "")] // Empty string
        public void Trim_ShouldRemoveSubstringFromBothEnds(string input, string trimString, string expected)
        {
            // Act
            var result = input.Trim(trimString);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("HelloWorldtest", "test", "HelloWorld")] // Single 'test' at the end
        [DataRow("HelloWorldtesttest", "test", "HelloWorld")] // Double 'test' at the end
        [DataRow("HelloWorld", "test", "HelloWorld")] // No 'test' at the end
        [DataRow("", "test", "")] // Empty string
        [DataRow("testtesttest", "test", "")] // Only 'test' in the string
        public void TrimEnd_ShouldRemoveSubstringFromEnd(string input, string trimString, string expected)
        {
            // Act
            var result = input.TrimEnd(trimString);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("testHelloWorld", "test", "HelloWorld")] // Single 'test' at the start
        [DataRow("testtestHelloWorld", "test", "HelloWorld")] // Double 'test' at the start
        [DataRow("HelloWorld", "test", "HelloWorld")] // No 'test' at the start
        [DataRow("", "test", "")] // Empty string
        [DataRow("testtesttest", "test", "")] // Only 'test' in the string
        public void TrimStart_ShouldRemoveSubstringFromStart(string input, string trimString, string expected)
        {
            // Act
            var result = input.TrimStart(trimString);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}