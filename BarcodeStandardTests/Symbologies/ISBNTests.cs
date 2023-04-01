using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;
using Type = BarcodeStandard.Type;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class ISBNTests
    {
        private readonly Barcode _barcode = new Barcode
        {
            EncodedType = Type.Isbn,
        };

        [DataTestMethod]
        [DataRow("978794729478", "10101110110001001001000100010110011101011101101010110110011101001011100100010010010001001110101")]
        [DataRow("9787947294785", "10101110110001001001000100010110011101011101101010110110011101001011100100010010010001001110101")]
        [DataRow("978791694478", "10101110110001001001000100010110110011010111101010111010010111001011100100010010010001110100101")]
        public void EncodeBarcode(
            string data,
            string expected)
        {
            try
            {
                _barcode.Encode(data);
            }
            catch when (expected == null)
            {
            }
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
