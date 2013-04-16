using BellaCode.Mvvm.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows;

namespace BellaCode.Mvvm.UnitTests.Converters
{
    [TestClass]
    public class InvertBooleanConverterTest
    {
        [TestMethod]
        public void WhenConstructedThenInitialized()
        {
            // Arrange

            // Act
            var actual = new InvertBooleanConverter();

            // Assert
            Assert.IsNotNull(actual);
        }       

        #region Convert Tests

        [TestMethod]
        public void WhenConvertCalledWithTrueThenFalseReturned()
        {
            // Arrange
            var expected = false;

            var value = true;

            var target = new InvertBooleanConverter();

            // Act
            var actual = target.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithFalseThenTrueReturned()
        {
            // Arrange
            var expected = true;

            var value = false;

            var target = new InvertBooleanConverter();

            // Act
            var actual = target.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullThenUnsetValueReturned()
        {
            // Arrange
            var expected = DependencyProperty.UnsetValue;

            var value = new object();

            var target = new InvertBooleanConverter();

            // Act
            var actual = target.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }    

        #endregion

        #region ConvertBack Tests

        [TestMethod]
        public void WhenConvertBackCalledWithTrueThenFalseReturned()
        {
            // Arrange
            var expected = false;

            var value = true;

            var target = new InvertBooleanConverter();

            // Act
            var actual = target.ConvertBack(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithFalseThenTrueReturned()
        {
            // Arrange
            var expected = true;

            var value = false;

            var target = new InvertBooleanConverter();

            // Act
            var actual = target.ConvertBack(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertBackCalledWithNullThenUnsetValueReturned()
        {
            // Arrange
            var expected = DependencyProperty.UnsetValue;

            var value = new object();

            var target = new InvertBooleanConverter();

            // Act
            var actual = target.ConvertBack(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }   

        #endregion     
    }
}
