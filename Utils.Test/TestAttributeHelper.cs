using System.ComponentModel.DataAnnotations;

namespace Utils.Test
{
    /// <summary>
    /// Test class for AttributeHelper
    /// </summary>
    [TestClass]
    public class TestAttributeHelper
    {
        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_NoAttributes()
        {
            Assert.IsNull(TestAttributeHelperEnum.NoAttributes.EnumAttributeFirstOrDefault<TestAttributeHelperAttribute>());
        }

        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_OneAttribute()
        {
            var attribute = TestAttributeHelperEnum.OneAttribute.EnumAttributeFirstOrDefault<TestAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("one", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_TwoAttributes()
        {
            var attribute = TestAttributeHelperEnum.TwoAttributes.EnumAttributeFirstOrDefault<TestAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("two_1", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeFirstOrDefault_TwoAttributesInOtherOrder()
        {
            var attribute = TestAttributeHelperEnum.TwoAttributesInOtherOrder.EnumAttributeFirstOrDefault<TestAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("three_2", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_NoAttributes()
        {
            Assert.IsNull(TestAttributeHelperEnum.NoAttributes.EnumAttributeSingleOrDefault<TestAttributeHelperAttribute>());
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_OneAttribute()
        {
            var attribute = TestAttributeHelperEnum.OneAttribute.EnumAttributeSingleOrDefault<TestAttributeHelperAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("one", attribute.SomeProperty);
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_TwoAttributes()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestAttributeHelperEnum.TwoAttributes.EnumAttributeSingleOrDefault<TestAttributeHelperAttribute>());
        }

        [TestMethod]
        public void TestEnumAttributeSingleOrDefault_TwoAttributesInOtherOrder()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestAttributeHelperEnum.TwoAttributesInOtherOrder.EnumAttributeSingleOrDefault<TestAttributeHelperAttribute>());
        }

    }

    enum TestAttributeHelperEnum
    {
        NoAttributes = 0,

        [TestAttributeHelper("one")]
        OneAttribute = 1,

        [TestAttributeHelper("two_1")]
        [TestAttributeHelper("two_2")]
        TwoAttributes = 2,

        [TestAttributeHelper("three_2")]
        [TestAttributeHelper("three_1")]
        TwoAttributesInOtherOrder = 3,
    }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="someProperty"></param>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    class TestAttributeHelperAttribute(string someProperty) : Attribute
    {
        /// <summary>
        /// Some property for testing purposes.
        /// </summary>
        public string SomeProperty { get; } = someProperty;
    }
}