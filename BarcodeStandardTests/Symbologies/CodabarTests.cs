using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class CodabarTests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.Codabar,
        };

        [DataTestMethod]
        [DataRow("A1927942B", "1011001001010101100101101001010101001011010010110101101001010101101001010100101101001001011")]
        [DataRow("A12345678901274B", "10110010010101011001010100101101100101010101101001011010100101001010110100101101010011010101101001010101010011010101100101010010110100101101010110100101001001011")]
        [DataRow("A192794729478B", "101100100101010110010110100101010100101101001011010110100101010110100101001011010101001011011010010101011010010100101101010011010101001001011")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
