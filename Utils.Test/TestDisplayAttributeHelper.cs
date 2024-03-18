using System.ComponentModel.DataAnnotations;

namespace Gradient.Utils.Test
{
    internal enum TestDisplayAttributeHelperEnum
    {
        Zero = 0,

        [Display]
        One = 1,

        [Display(Name = "Name", Description = "Description", ShortName = "ShortName", Prompt = "Prompt", GroupName = "GroupName")]
        Two = 2,
    }

    [TestClass]
    public class TestDisplayAttributeHelper
    {
        [TestMethod]
        public void TestDisplayAttributeMissingAttribute_Description()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.Zero.DisplayDescriptionOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingAttribute_GroupName()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.Zero.DisplayGroupNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingAttribute_Name()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.Zero.DisplayNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingAttribute_Prompt()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.Zero.DisplayPromptOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingAttribute_ShortName()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.Zero.DisplayShortNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingProperty_Description()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.One.DisplayDescriptionOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingProperty_GroupName()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.One.DisplayGroupNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingProperty_Name()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.One.DisplayNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingProperty_Prompt()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.One.DisplayPromptOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeMissingProperty_ShortName()
        {
            Assert.AreEqual("", TestDisplayAttributeHelperEnum.One.DisplayShortNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeOk_Description()
        {
            Assert.AreEqual("Description", TestDisplayAttributeHelperEnum.Two.DisplayDescriptionOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeOk_GroupName()
        {
            Assert.AreEqual("GroupName", TestDisplayAttributeHelperEnum.Two.DisplayGroupNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeOk_Name()
        {
            Assert.AreEqual("Name", TestDisplayAttributeHelperEnum.Two.DisplayNameOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeOk_Prompt()
        {
            Assert.AreEqual("Prompt", TestDisplayAttributeHelperEnum.Two.DisplayPromptOrDefault());
        }

        [TestMethod]
        public void TestDisplayAttributeOk_ShortName()
        {
            Assert.AreEqual("ShortName", TestDisplayAttributeHelperEnum.Two.DisplayShortNameOrDefault());
        }
    }
}