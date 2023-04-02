using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class PostnetTests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.PostNet,
        };

        [DataTestMethod]
        [DataRow("19283", "10001110100001011001000110100011")]
        [DataRow("192837", "1000111010000101100100011010001110001")]
        [DataRow("192837", "1000111010000101100100011010001110001")]
        [DataRow("123456789", "1000110010100110010010101001100100011001010100010101")]
        [DataRow("456789012", "1010010101001100100011001010100110000001100101100101")]
        [DataRow("19283744324", "10001110100001011001000110100010100101001001100010101001001101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
