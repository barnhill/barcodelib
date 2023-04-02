using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class Itf14Tests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.Itf14,
        };

        [DataTestMethod]
        [DataRow("1927947294781", "1010110100101001101011010100110010110100110100101001011011001011010011010010010101100110110010101001101101")]
        [DataRow("1234567890127", "1010110100101011001101101001010011010011001010100101011001101011010011001011010010101100101001011001101101")]
        [DataRow("1927947294784", "1010110100101001101011010100110010110100110100101001011011001011010011010010010101100110101001101001101101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
