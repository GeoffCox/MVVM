using BellaCode.Mvvm.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Windows;

namespace BellaCode.Mvvm.UnitTests.Converters
{
    [TestClass]
    public class BooleanToKnownValueConverterTest
    {
        [TestMethod]
        public void WhenConstructedThenInitialized()
        {
            // Arrange

            // Act
            var actual = new BooleanToKnownValueConverter();

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void WhenWhenTrueSetThenValueUpdated()
        {
            // Arrange
            var expected = new object();

            var target = new BooleanToKnownValueConverter();

            // Act
            target.WhenTrue = expected;
            var actual = target.WhenTrue;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void WhenWhenFalseSetThenValueUpdated()
        {
            // Arrange
            var expected = new object();

            var target = new BooleanToKnownValueConverter();

            // Act
            target.WhenFalse = expected;
            var actual = target.WhenFalse;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void WhenWhenNullSetThenValueUpdated()
        {
            // Arrange
            var expected = new object();

            var target = new BooleanToKnownValueConverter();

            // Act
            target.WhenNull = expected;
            var actual = target.WhenNull;

            // Assert
            Assert.AreSame(expected, actual);
        }

        #region Convert Tests

        [TestMethod]
        public void WhenConvertCalledWithTrueThenWhenTrueReturned()
        {
            // Arrange
            var expected = new object();

            var value = true;

            var target = new BooleanToKnownValueConverter();
            target.WhenTrue = expected;

            // Act
            var actual = target.Convert(value, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithFalseThenWhenFalseReturned()
        {
            // Arrange
            var expected = new object();

            var value = false;

            var target = new BooleanToKnownValueConverter();
            target.WhenFalse = expected;

            // Act
            var actual = target.Convert(value, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullThenWhenNullReturned()
        {
            // Arrange
            var expected = new object();

            var value = (object)null;

            var target = new BooleanToKnownValueConverter();
            target.WhenNull = expected;

            // Act
            var actual = target.Convert(value, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreSame(expected, actual);
        }

        #endregion

        #region ConvertBack Tests

        [TestMethod]
        public void WhenConvertBackCalledWithWhenTrueThenTrueReturned()
        {
            // Arrange
            var expected = true;

            var value = new object();

            var target = new BooleanToKnownValueConverter();
            target.WhenTrue = value;

            // Act
            var actual = target.ConvertBack(value, typeof(bool?), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithWhenFalseThenFalseReturned()
        {
            // Arrange
            var expected = (bool?)false;

            var value = new object();

            var target = new BooleanToKnownValueConverter();
            target.WhenFalse = value;

            // Act
            var actual = target.ConvertBack(value, typeof(bool?), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithWhenNullThenNullReturned()
        {
            // Arrange
            var expected = (object)null;

            var value = new object();

            var target = new BooleanToKnownValueConverter();
            target.WhenNull = value;

            // Act
            var actual = target.ConvertBack(value, typeof(bool?), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithNonMatchThenUnsetValueReturned()
        {
            // Arrange
            var expected = DependencyProperty.UnsetValue;

            var value = new object();

            var target = new BooleanToKnownValueConverter();

            // Act
            var actual = target.ConvertBack(value, typeof(bool?), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
