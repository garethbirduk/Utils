namespace Gradient.Utils.Test
{
    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow("hello hello hello", "hello", "hi", 2, "hi hi hello")] // Replace first 2 instances
        [DataRow("hello hello hello", "hello", "hi", 5, "hi hi hi")] // Replace all instances
        [DataRow("hello hello hello", "world", "hi", 2, "hello hello hello")] // No match
        public void ReplaceFirst_MaxInstances_Test(string input, string search, string replace, int maxInstances, string expected)
        {
            var result = input.ReplaceFirst(search, replace, maxInstances);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("hello world", "hello", "hi", "hi world")] // First occurrence
        [DataRow("hello hello world", "hello", "hi", "hi hello world")] // First occurrence in multiple
        [DataRow("hello world", "test", "hi", "hello world")] // No match
        [DataRow("", "test", "hi", "")] // Empty input
        public void ReplaceFirst_Test(string input, string search, string replace, string expected)
        {
            var result = input.ReplaceFirst(search, replace);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("hello hello hello", "hello", "hi", 2, "hello hi hi")] // Replace last 2 instances
        [DataRow("hello hello hello", "hello", "hi", 5, "hi hi hi")] // Replace all instances
        [DataRow("hello hello hello", "world", "hi", 2, "hello hello hello")] // No match
        public void ReplaceLast_MaxInstances_Test(string input, string search, string replace, int maxInstances, string expected)
        {
            var result = input.ReplaceLast(search, replace, maxInstances);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("hello world", "world", "planet", "hello planet")] // Last occurrence
        [DataRow("hello world world", "world", "planet", "hello world planet")] // Last occurrence in multiple
        [DataRow("hello world", "test", "hi", "hello world")] // No match
        [DataRow("", "test", "hi", "")] // Empty input
        public void ReplaceLast_Test(string input, string search, string replace, string expected)
        {
            var result = input.ReplaceLast(search, replace);
            Assert.AreEqual(expected, result);
        }
    }
}