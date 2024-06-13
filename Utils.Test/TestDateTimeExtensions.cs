using System.Globalization;

namespace Gradient.Utils.Test
{
    [TestClass]
    public class TestDateTimeExtensions
    {
        [DataTestMethod]
        [DataRow("Mon 1 Jan", 2024, 1, 1)]
        [DataRow("Mon 01 Jan", 2024, 1, 1)]
        [DataRow("1 Jan", 2024, 1, 1)]
        [DataRow("01 Jan", 2024, 1, 1)]
        [DataRow("Monday 1 Jan", 2024, 1, 1)]
        [DataRow("Monday 01 Jan", 2024, 1, 1)]
        [DataRow("Monday 1 Jan 2024", 2024, 1, 1)]
        [DataRow("Monday 01 Jan 2024", 2024, 1, 1)]
        [DataRow("Mon 1 January", 2024, 1, 1)]
        [DataRow("Mon 01 January", 2024, 1, 1)]
        [DataRow("1 January", 2024, 1, 1)]
        [DataRow("01 January", 2024, 1, 1)]
        [DataRow("Monday 1 January", 2024, 1, 1)]
        [DataRow("Monday 01 January", 2024, 1, 1)]
        [DataRow("Monday 1 January 2024", 2024, 1, 1)]
        [DataRow("Monday 01 January 2024", 2024, 1, 1)]
        [DataRow("01/01/2024", 2024, 1, 1)]
        [DataRow("01/1/2024", 2024, 1, 1)]
        [DataRow("1/01/2024", 2024, 1, 1)]
        [DataRow("1/1/2024", 2024, 1, 1)]
        [DataRow("2024/01/01", 2024, 1, 1)]
        [DataRow("2024/1/01", 2024, 1, 1)]
        [DataRow("2024/01/1", 2024, 1, 1)]
        [DataRow("2024/1/1", 2024, 1, 1)]
        [DataRow("2024-01-01", 2024, 1, 1)]
        [DataRow("2024-1-01", 2024, 1, 1)]
        [DataRow("2024-01-1", 2024, 1, 1)]
        [DataRow("2024-1-1", 2024, 1, 1)]
        [DataRow("01/01/24", 2024, 1, 1)]
        [DataRow("01/1/24", 2024, 1, 1)]
        [DataRow("1/01/24", 2024, 1, 1)]
        [DataRow("1/1/24", 2024, 1, 1)]
        [DataRow("1-Jan-24", 2024, 1, 1)]
        [DataRow("01-Jan-24", 2024, 1, 1)]
        [DataRow("1-January-24", 2024, 1, 1)]
        [DataRow("01-January-24", 2024, 1, 1)]
        [DataRow("1-Jan-2024", 2024, 1, 1)]
        [DataRow("01-Jan-2024", 2024, 1, 1)]
        [DataRow("1-January-2024", 2024, 1, 1)]
        [DataRow("01-January-2024", 2024, 1, 1)]
        [DataRow("Jan 1, 2024", 2024, 1, 1)]
        [DataRow("January 1, 2024", 2024, 1, 1)]
        [DataRow("Jan 01, 2024", 2024, 1, 1)]
        [DataRow("January 01, 2024", 2024, 1, 1)]
        [DataRow("Monday 30th Dec 2024", 2024, 12, 30)]
        [DataRow("Friday 14th June 2024", 2024, 06, 14)]
        [DataRow("Thursday 13th June 2024", 2024, 06, 13)]
        [DataRow("Thursday 13th Jun 2024", 2024, 06, 13)]
        public void TestParseAdvanced(string input, int expectedYear, int expectedMonth, int expectedDay)
        {
            // Arrange
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay);

            // Act
            var result = input.ParseAdvanced();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestParseAdvanced_CustomCultureInfo_ParsesCorrectly()
        {
            // Arrange
            var date = "01/01/2024";
            var cultureInfo = new CultureInfo("en-GB"); // dd/MM/yyyy format

            // Act
            var result = date.ParseAdvanced(cultureInfo);

            // Assert
            Assert.AreEqual(new DateTime(2024, 1, 1), result);
        }

        [TestMethod]
        public void TestParseAdvanced_FallsBackToDefaultParse()
        {
            // Arrange
            var date = "January 1, 2024";

            // Act
            var result = date.ParseAdvanced();

            // Assert
            Assert.AreEqual(new DateTime(2024, 1, 1), result);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseAdvanced_InvalidDateFormat_ThrowsFormatException()
        {
            // Arrange
            var invalidDate = "Invalid Date String";

            // Act
            invalidDate.ParseAdvanced();
        }

        [DataTestMethod]
        [DataRow("24/01/01", 2001, 1, 24)]
        [DataRow("24/01/1", 2001, 1, 24)]
        [DataRow("24/1/1", 2001, 1, 24)]
        public void TestParseAdvanced_USFormatsNotSupported(string input, int expectedYear, int expectedMonth, int expectedDay)
        {
            // Arrange
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay);

            // Act
            var result = input.ParseAdvanced();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// This unit test may need updating from time to time because the expected behaviour depends on DateTime.UtcNow()
        /// </summary>
        [TestMethod]
        public void TestParseAdvanced_YearMissingExample1()
        {
            // this date is true in 2024 (current year)
            var date = "Mon 1 Jan";

            // Arrange
            var expected = new DateTime(2024, 01, 01);

            // Act
            var result = date.ParseAdvanced();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// This unit test may need updating from time to time because the expected behaviour depends on DateTime.UtcNow()
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseAdvanced_YearMissingExample2()
        {
            // this date is true in 2023 and not in 2024 (current year)
            var date = "Sun 1 Jan";

            // Arrange
            var expected = new DateTime(2023, 01, 01);

            // Act
            var result = date.ParseAdvanced();
        }
    }
}