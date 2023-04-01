using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;
using Type = BarcodeStandard.Type;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class EAN13Tests
    {
        private readonly Barcode _barcode = new Barcode
        {
            EncodedType = Type.Ean13,
        };

        [DataTestMethod]
        [DataRow("038000356216", "10101111010110111000110100011010001101011110101010100111010100001101100110011010100001011100101")]
        [DataRow("123456789012", "10100100110111101001110101100010000101001000101010100100011101001110010110011011011001001000101")]
        [DataRow("192794729478", "10100010110010011001000100010110011101001000101010110110011101001011100100010010010001000010101")]
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
