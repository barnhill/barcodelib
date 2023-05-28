using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class Interleaved2Of5Tests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.Interleaved2Of5,
        };

        [DataTestMethod]
        [DataRow("19279472947812", "1010110100101001101011010100110010110100110100101001011011001011010011010010010101100110110100101011001101")]
        [DataRow("12345678901274", "1010110100101011001101101001010011010011001010100101011001101011010011001011010010101100101010011011001101")]
        [DataRow("192794729478", "10101101001010011010110101001100101101001101001010010110110010110100110100100101011001101101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
