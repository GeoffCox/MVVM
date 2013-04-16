using BellaCode.Mvvm.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows;

namespace BellaCode.Mvvm.UnitTests.Converters
{
    [TestClass]
    public class EnumVisibilityConverterTest
    {
        [TestMethod]
        public void WhenConstructedThenInitialized()
        {
            // Arrange

            // Act
            var actual = new EnumVisibilityConverter();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Visibility.Visible, actual.VisibilityWhenEqual);
            Assert.AreEqual(Visibility.Hidden, actual.VisibilityWhenNotEqual);
        }

        [TestMethod]
        public void WhenVisibilityWhenEqualSetThenValueUpdated()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var target = new EnumVisibilityConverter();

            // Act
            target.VisibilityWhenEqual = expected;
            var actual = target.VisibilityWhenEqual;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenWhenFalseSetThenValueUpdated()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var target = new EnumVisibilityConverter();

            // Act
            target.VisibilityWhenNotEqual = expected;
            var actual = target.VisibilityWhenNotEqual;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #region Convert Tests

        [TestMethod]
        public void WhenConvertCalledWithMatchingValueThenVisibilityWhenEqualReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = DummyEnum.Value2;
            var converterParameter = DummyEnum.Value2;

            var target = new EnumVisibilityConverter();
            target.VisibilityWhenEqual = Visibility.Collapsed;

            // Act
            var actual = target.Convert(value, typeof(Visibility), converterParameter, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithDifferentValueThenVisibilityWhenNotEqualReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = DummyEnum.Value2;
            var converterParameter = DummyEnum.Value1;

            var target = new EnumVisibilityConverter();
            target.VisibilityWhenNotEqual = Visibility.Collapsed;

            // Act
            var actual = target.Convert(value, typeof(Visibility), converterParameter, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullConverterParameterThenFalseReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = DummyEnum.Value2;
            var converterParameter = (object)null;

            var target = new EnumVisibilityConverter();
            target.VisibilityWhenNotEqual = Visibility.Collapsed;

            // Act
            var actual = target.Convert(value, typeof(Visibility), converterParameter, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullValueThenFalseReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = (object)null;
            var converterParameter = DummyEnum.Value1;

            var target = new EnumVisibilityConverter();
            target.VisibilityWhenNotEqual = Visibility.Collapsed;

            // Act
            var actual = target.Convert(value, typeof(Visibility), converterParameter, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ConvertBack Tests

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void WhenConvertBackCalledThenExceptionThrown()
        {
            // Arrange
            var value = Visibility.Visible;
            var converterParameter = DummyEnum.Value1;

            var target = new EnumVisibilityConverter();

            // Act
            var actual = target.ConvertBack(value, typeof(DummyEnum), converterParameter, CultureInfo.InvariantCulture);

            // Assert
        }

        #endregion

        private enum DummyEnum
        {
            Value1,
            Value2,
            Value3
        }
    }
}
