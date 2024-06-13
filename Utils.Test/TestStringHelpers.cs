namespace Gradient.Utils.Test
{
    /// <summary>
    /// Test class for StringHelpers
    /// </summary>
    [TestClass]
    public class TestStringHelpers
    {
        [TestMethod]
        public void TestRandomName()
        {
            Assert.AreEqual(8, StringHelpers.RandomName().Length);

            for (var i = 1; i < 37; i++)
                Assert.AreEqual(i, StringHelpers.RandomName(length: i).Length);
        }

        [TestMethod]
        public void TestRandomNameDefault()
        {
            Assert.AreEqual(8, StringHelpers.RandomName().Length);
        }

        [TestMethod]
        public void TestRandomNamePrefix()
        {
            Assert.IsTrue(StringHelpers.RandomName(prefix: "bob").StartsWith("bob"));
        }

        [TestMethod]
        public void TestRandomNameSuffix()
        {
            Assert.IsTrue(StringHelpers.RandomName(suffix: "bob").EndsWith("bob"));
        }

        [TestMethod]
        public void TestRandomNameTooLong()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => StringHelpers.RandomName(length: 100));
        }
    }
}