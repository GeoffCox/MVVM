using BellaCode.Mvvm.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Windows;

namespace BellaCode.Mvvm.UnitTests.Converters
{
    [TestClass]
    public class BooleanVisibilityConverterTest
    {
        [TestMethod]
        public void WhenConstructedThenInitialized()
        {
            // Arrange

            // Act
            var actual = new BooleanVisibilityConverter();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Visibility.Visible, actual.WhenTrue);
            Assert.AreEqual(Visibility.Hidden, actual.WhenFalse);
            Assert.AreEqual(Visibility.Hidden, actual.WhenNull);
        }

        [TestMethod]
        public void WhenWhenTrueSetThenValueUpdated()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var target = new BooleanVisibilityConverter();

            // Act
            target.WhenTrue = expected;
            var actual = target.WhenTrue;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenWhenFalseSetThenValueUpdated()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var target = new BooleanVisibilityConverter();

            // Act
            target.WhenFalse = expected;
            var actual = target.WhenFalse;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenWhenNullSetThenValueUpdated()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var target = new BooleanVisibilityConverter();

            // Act
            target.WhenNull = expected;
            var actual = target.WhenNull;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #region Convert Tests

        [TestMethod]
        public void WhenConvertCalledWithTrueThenWhenTrueReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = true;

            var target = new BooleanVisibilityConverter();
            target.WhenTrue = expected;

            // Act
            var actual = target.Convert(value, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithFalseThenWhenFalseReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = false;

            var target = new BooleanVisibilityConverter();
            target.WhenFalse = expected;

            // Act
            var actual = target.Convert(value, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullThenWhenNullReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = (object)null;

            var target = new BooleanVisibilityConverter();
            target.WhenNull = expected;

            // Act
            var actual = target.Convert(value, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ConvertBack Tests

        [TestMethod]
        public void WhenConvertBackCalledWithWhenTrueThenTrueReturned()
        {
            // Arrange
            var expected = true;

            var value = Visibility.Collapsed;

            var target = new BooleanVisibilityConverter();
            target.WhenTrue = value;

            // Act
            var actual = target.ConvertBack(value, typeof(Visibility), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithWhenFalseThenFalseReturned()
        {
            // Arrange
            var expected = (bool?)false;

            var value = Visibility.Collapsed;

            var target = new BooleanVisibilityConverter();
            target.WhenFalse = value;

            // Act
            var actual = target.ConvertBack(value, typeof(Visibility), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithWhenNullThenNullReturned()
        {
            // Arrange
            var expected = (object)null;

            var value = Visibility.Collapsed;

            var target = new BooleanVisibilityConverter();
            target.WhenNull = value;

            // Act
            var actual = target.ConvertBack(value, typeof(Visibility), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithNonMatchThenUnsetValueReturned()
        {
            // Arrange
            var expected = DependencyProperty.UnsetValue;

            var value = Visibility.Collapsed;

            var target = new BooleanVisibilityConverter();

            // Act
            var actual = target.ConvertBack(value, typeof(bool?), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
