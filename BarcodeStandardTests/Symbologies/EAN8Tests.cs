using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;
using Type = BarcodeStandard.Type;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class EAN8Tests
    {
        private readonly Barcode _barcode = new Barcode
        {
            EncodedType = Type.Ean8,
        };

        [DataTestMethod]
        [DataRow("7294783", "1010111011001001100010110100011010101000100100100010000101001000101")]
        [DataRow("1789012", "1010011001011101101101110001011010101110010110011011011001110010101")]
        [DataRow("4729478", "1010100011011101100100110001011010101011100100010010010001000010101")]
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
