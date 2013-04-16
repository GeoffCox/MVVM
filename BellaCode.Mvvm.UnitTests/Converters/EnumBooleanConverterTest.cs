using BellaCode.Mvvm.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace BellaCode.Mvvm.UnitTests.Converters
{
    [TestClass]
    public class EnumBooleanConverterTest
    {
        [TestMethod]
        public void WhenConstructedThenInitialized()
        {
            // Arrange

            // Act
            var actual = new EnumBooleanConverter();

            // Assert
            Assert.IsNotNull(actual);
        }

        #region Convert Tests

        [TestMethod]
        public void WhenConvertCalledWithMatchingValueThenTrueReturned()
        {
            // Arrange
            var expected = true;

            var value = DummyEnum.Value2;
            var converterParameter = DummyEnum.Value2;

            var target = new EnumBooleanConverter();            

            // Act
            var actual = target.Convert(value, typeof(bool), converterParameter, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithDifferentValueThenFalseReturned()
        {
            // Arrange
            var expected = false;

            var value = DummyEnum.Value2;
            var converterParameter = DummyEnum.Value1;

            var target = new EnumBooleanConverter();

            // Act
            var actual = target.Convert(value, typeof(bool), converterParameter, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullConverterParameterThenFalseReturned()
        {
            // Arrange
            var expected = false;

            var value = DummyEnum.Value2;

            var target = new EnumBooleanConverter();
            var converterParameter = (object)null;

            // Act
            var actual = target.Convert(value, typeof(bool), converterParameter, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullValueThenFalseReturned()
        {
            // Arrange
            var expected = false;

            var value = (object)null;
            var converterParameter = DummyEnum.Value1;

            var target = new EnumBooleanConverter();

            // Act
            var actual = target.Convert(value, typeof(bool), converterParameter, CultureInfo.InvariantCulture);

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
            var converterParameter = DummyEnum.Value1;

            var target = new EnumBooleanConverter();

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
