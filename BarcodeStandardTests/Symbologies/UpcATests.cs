using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class UpcATests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.UpcA,
        };

        [DataTestMethod]
        [DataRow("038000356216", "10100011010111101011011100011010001101000110101010100001010011101010000110110011001101010000101")]
        [DataRow("123456789012", "10100110010010011011110101000110110001010111101010100010010010001110100111001011001101101100101")]
        [DataRow("192794729478", "10100110010001011001001101110110001011010001101010100010011011001110100101110010001001110100101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
