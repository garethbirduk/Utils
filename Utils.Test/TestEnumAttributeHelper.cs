using System;

namespace Gradient.Utils.Test
{
    internal enum TestEnumAttributeHelperExampleEnum
    {
        NoAttributes = 0,

        [TestEnumAttributeHelper("one")]
        OneAttribute = 1,

        [TestEnumAttributeHelper("two_1")]
        [TestEnumAttributeHelper("two_2")]
        TwoAttributes = 2,

        [TestEnumAttributeHelper("three_2")]
        [TestEnumAttributeHelper("three_1")]
        TwoAttributesInOtherOrder = 3,
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="someProperty"></param>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    internal class TestEnumAttributeHelperAttribute(string someProperty) : Attribute
    {
        /// <summary>
        /// Some property for testing purposes.
        /// </summary>
        public string SomeProperty { get; } = someProperty;
    }

    /// <summary>
    /// Test class for EnumAttributeHelper
    /// </summary>
    [TestClass]
    public class TestEnumAttributeHelper
    {
        /// <summary>
        /// Theoretical case where myEnum is obtained from an external source or user input, is cast to an enum value that does not exist
        /// </summary>
        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_FalseEnumCast()
        {
            Enum myEnum = (TestEnumAttributeHelperExampleEnum)99;
            Assert.IsNull(myEnum.EnumAttributes<Attribute>().FirstOrDefault());
        }

        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_NoAttributes()
        {
            Assert.IsNull(TestEnumAttributeHelperExampleEnum.NoAttributes.EnumAttributeFirstOrDefault<TestEnumAttributeHelperAttribute>());
        }

        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_OneAttribute()
        {
            var attribute = TestEnumAttributeHelperExampleEnum.OneAttribute.EnumAttributeFirstOrDefault<TestEnumAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("one", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_TwoAttributes()
        {
            var attribute = TestEnumAttributeHelperExampleEnum.TwoAttributes.EnumAttributeFirstOrDefault<TestEnumAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("two_1", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_TwoAttributesInOtherOrder()
        {
            var attribute = TestEnumAttributeHelperExampleEnum.TwoAttributesInOtherOrder.EnumAttributeFirstOrDefault<TestEnumAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("three_2", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_NoAttributes()
        {
            Assert.IsNull(TestEnumAttributeHelperExampleEnum.NoAttributes.EnumAttributeSingleOrDefault<TestEnumAttributeHelperAttribute>());
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_OneAttribute()
        {
            var attribute = TestEnumAttributeHelperExampleEnum.OneAttribute.EnumAttributeSingleOrDefault<TestEnumAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("one", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_TwoAttributes()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestEnumAttributeHelperExampleEnum.TwoAttributes.EnumAttributeSingleOrDefault<TestEnumAttributeHelperAttribute>());
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_TwoAttributesInOtherOrder()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestEnumAttributeHelperExampleEnum.TwoAttributesInOtherOrder.EnumAttributeSingleOrDefault<TestEnumAttributeHelperAttribute>());
        }
    }
}