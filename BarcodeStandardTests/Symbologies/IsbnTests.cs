using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class IsbnTests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.Isbn,
        };

        [DataTestMethod]
        [DataRow("978794729478", "10101110110001001001000100010110011101011101101010110110011101001011100100010010010001001110101")]
        [DataRow("9787947294785", "10101110110001001001000100010110011101011101101010110110011101001011100100010010010001001110101")]
        [DataRow("978791694478", "10101110110001001001000100010110110011010111101010111010010111001011100100010010010001110100101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
