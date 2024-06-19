using Gradient.Utils.Attributes;
using System.Linq.Dynamic.Core.Exceptions;

namespace Gradient.Utils.Test
{
    internal static class MyTestClass
    {
        [Precondition("myInt", "myInt > 10", "my error message")]
        public static bool CustomErrorMessage(int myInt)
        {
            return myInt < 1000;
        }

        [Precondition("myInt", "myInt > 10")]
        public static bool DefaultErrorMessage(int myInt)
        {
            return myInt < 1000;
        }

        [Precondition("myInt2", "null")]
        public static bool GivesParseError(int myInt2)
        {
            return myInt2 < 50;
        }

        [Precondition("myInt", "myInt > 10")]
        public static bool IsLessThan100(int myInt)
        {
            return myInt < 100;
        }

        [Precondition("a", "a > 0")]
        [Precondition("b", "b > 0")]
        public static int MultiplePreconditions(int a, int b)
        {
            return a + b;
        }

        [Precondition("myThing", "myThing != null && myThing.MyCount < 100")]
        public static bool MyObjectMethod(Thing myThing)
        {
            return myThing is null;
        }
    }

    [TestClass]
    public class PreconditionAttributeTests
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.IsNotNull(new PreconditionAttribute("a", "x != 1", "my message"));
            Assert.IsNotNull(new PreconditionAttribute("a", "x != 1"));
        }

        [TestMethod]
        public void Test_CustomErrorMessage()
        {
            var exceptionCaught = false;
            try
            {
                MyTestClass.CustomErrorMessage(5);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
                Assert.IsTrue(ex.Message.Contains("my error message"));
            }
            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        public void Test_DefaultErrorMessage()
        {
            var exceptionCaught = false;
            try
            {
                MyTestClass.DefaultErrorMessage(5);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
                Assert.IsFalse(ex.Message.Contains("my error message"));
                Assert.IsTrue(ex.Message.Contains("Parameter preconditions not met: myInt > 10"));
            }
            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Multiple_BothFail()
        {
            MyTestClass.MultiplePreconditions(-2, -3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Multiple_BothFails()
        {
            Assert.AreEqual(5, MyTestClass.MultiplePreconditions(2, -3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Multiple_FirstFails()
        {
            MyTestClass.MultiplePreconditions(-2, 3);
        }

        [TestMethod]
        public void Test_Multiple_OK()
        {
            Assert.AreEqual(5, MyTestClass.MultiplePreconditions(2, 3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Multiple_SecondFails()
        {
            MyTestClass.MultiplePreconditions(2, -3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MyIntMethod_xTooLow()
        {
            MyTestClass.IsLessThan100(5);
        }

        [TestMethod]
        public void TestIsNotNullExample_Error_IsNull()
        {
            Assert.ThrowsException<ArgumentException>(() => MyTestClass.MyObjectMethod(null));
        }

        [TestMethod]
        public void TestIsNotNullExample_Error_MyCountTooHigh()
        {
            Assert.ThrowsException<ArgumentException>(() => MyTestClass.MyObjectMethod(new Thing() { MyCount = 999 }));
        }

        [TestMethod]
        public void TestIsNotNullExample_NoError()
        {
            Assert.IsFalse(MyTestClass.MyObjectMethod(new Thing() { MyCount = 99 }));
        }

        [TestMethod]
        public void TestIsNotNullExample_ParseError()
        {
            Assert.ThrowsException<ParseException>(() => MyTestClass.GivesParseError(25));
        }

        [TestMethod]
        public void TestMyIntMethod_NoError()
        {
            Assert.IsTrue(MyTestClass.IsLessThan100(55));
            Assert.IsFalse(MyTestClass.IsLessThan100(555));
        }
    }

    public class Thing
    {
        public int MyCount { get; set; } = 99;
    }
}