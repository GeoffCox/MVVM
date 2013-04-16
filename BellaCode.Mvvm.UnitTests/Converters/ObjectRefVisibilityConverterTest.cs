using BellaCode.Mvvm.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows;

namespace BellaCode.Mvvm.UnitTests.Converters
{
    [TestClass]
    public class ObjectRefVisibilityConverterTest
    {
        [TestMethod]
        public void WhenConstructedThenInitialized()
        {
            // Arrange

            // Act
            var actual = new ObjectRefVisibilityConverter();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Visibility.Visible, actual.WhenNotNull);
            Assert.AreEqual(Visibility.Hidden, actual.WhenNull);
        }

        [TestMethod]
        public void WhenWhenNotNullSetThenValueUpdated()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var target = new ObjectRefVisibilityConverter();

            // Act
            target.WhenNotNull = expected;
            var actual = target.WhenNotNull;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenWhenNullSetThenValueUpdated()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var target = new ObjectRefVisibilityConverter();

            // Act
            target.WhenNull = expected;
            var actual = target.WhenNull;

            // Assert
            Assert.AreEqual(expected, actual);
        }        

        #region Convert Tests

        [TestMethod]
        public void WhenConvertCalledWithNonNullValueThenWhenNotNullReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = new object();

            var target = new ObjectRefVisibilityConverter();
            target.WhenNotNull = expected;

            // Act
            var actual = target.Convert(value, typeof(Visibility), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullValueThenWhenNullReturned()
        {
            // Arrange
            var expected = Visibility.Collapsed;

            var value = (object)null;

            var target = new ObjectRefVisibilityConverter();
            target.WhenNull = expected;

            // Act
            var actual = target.Convert(value, typeof(Visibility), null, CultureInfo.InvariantCulture);

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
            var value = true;
            var converterParameter = new object();

            var target = new ObjectRefVisibilityConverter();

            // Act
            var actual = target.ConvertBack(value, typeof(object), converterParameter, CultureInfo.InvariantCulture);

            // Assert
        }

        #endregion
    }
}
