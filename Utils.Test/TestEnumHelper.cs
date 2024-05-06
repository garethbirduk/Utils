using Gradient.Utils.Attributes;

namespace Gradient.Utils.Test
{
    public enum MyColors
    {
        [Alias("red", "rouge", "root")]
        Red,

        Green
    }

    public enum MyEnum
    {
        No,

        [Alias("Si")]
        Yes,
    }

    [TestClass]
    public class TestStringToEnumHelper
    {
        [DataTestMethod]
        [DataRow(MyEnum.Yes, 1)]
        [DataRow(MyEnum.No, 0)]
        [DataRow(MyColors.Red, 3)]
        public void TestAliases(Enum myEnum, int expected)
        {
            Assert.AreEqual(expected, EnumHelper.Aliases(myEnum).Count());
        }

        [DataTestMethod]
        [DataRow(MyEnum.Yes, "Si")]
        [DataRow(MyEnum.No, null)]
        [DataRow(MyColors.Red, "red")]
        public void TestAliasFirstOrDefault(Enum myEnum, string expected)
        {
            Assert.AreEqual(expected, EnumHelper.AliasFirstOrDefault(myEnum));
        }

        [DataTestMethod]
        [DataRow(MyEnum.Yes, "Si")]
        [DataRow(MyEnum.No, null)]
        public void TestAliasSingleOrDefault(Enum myEnum, string expected)
        {
            Assert.AreEqual(expected, EnumHelper.AliasSingleOrDefault(myEnum));
        }

        [DataTestMethod]
        [DataRow(MyColors.Red)]
        public void TestAliasSingleOrDefault_Fails(Enum myEnum)
        {
            Assert.ThrowsException<InvalidOperationException>(() => EnumHelper.AliasSingleOrDefault(myEnum));
        }

        [DataTestMethod]
        [DataRow("Yes", MyEnum.No)]
        [DataRow("No", MyEnum.No)]
        [DataRow("Si", MyEnum.Yes)]
        [DataRow("Ja", MyEnum.No)]
        public void TestStringToEnumAliasOrDefault(string enumString, MyEnum expected)
        {
            Assert.AreEqual(expected, EnumHelper.StringToEnumAliasOrDefault<MyEnum>(enumString));
        }

        [DataTestMethod]
        [DataRow("Yes", false, MyEnum.Yes)]
        [DataRow("No", false, MyEnum.No)]
        [DataRow("Si", false, MyEnum.No)]
        [DataRow("Ja", false, MyEnum.No)]
        [DataRow("Yes", true, MyEnum.Yes)]
        [DataRow("No", true, MyEnum.No)]
        [DataRow("Si", true, MyEnum.Yes)]
        [DataRow("Ja", true, MyEnum.No)]
        public void TestStringToEnumOrDefault(string enumString, bool allowAlias, MyEnum expected)
        {
            Assert.AreEqual(expected, EnumHelper.StringToEnumOrDefault<MyEnum>(enumString, allowAlias));
        }
    }
}