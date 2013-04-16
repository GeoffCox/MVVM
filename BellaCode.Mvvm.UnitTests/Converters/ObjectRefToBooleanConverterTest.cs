using BellaCode.Mvvm.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace BellaCode.Mvvm.UnitTests.Converters
{
    [TestClass]
    public class ObjectRefToBooleanConverterTest
    {
        [TestMethod]
        public void WhenConstructedThenInitialized()
        {
            // Arrange

            // Act
            var actual = new ObjectRefToBooleanConverter();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.WhenNotNull);
            Assert.IsFalse(actual.WhenNull);
        }

        [TestMethod]
        public void WhenWhenNotNullSetThenValueUpdated()
        {
            // Arrange
            var expected = false;

            var target = new ObjectRefToBooleanConverter();

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
            var expected = true;

            var target = new ObjectRefToBooleanConverter();

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
            var expected = false;

            var value = new object();

            var target = new ObjectRefToBooleanConverter();
            target.WhenNotNull = false;
            target.WhenNull = true;

            // Act
            var actual = target.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenConvertCalledWithNullValueThenWhenNullReturned()
        {
            // Arrange
            var expected = true;

            var value = (object)null;

            var target = new ObjectRefToBooleanConverter();
            target.WhenNotNull = false;
            target.WhenNull = true;

            // Act
            var actual = target.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture);

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

            var target = new ObjectRefToBooleanConverter();

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
