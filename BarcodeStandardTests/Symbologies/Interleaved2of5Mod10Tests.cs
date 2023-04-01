using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;
using Type = BarcodeStandard.Type;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class Interleaved2of5Mod10Tests
    {
        private readonly Barcode _barcode = new Barcode
        {
            EncodedType = Type.Interleaved2Of5Mod10,
        };

        [DataTestMethod]
        [DataRow("192794729478122", "101011010010100110101101010011001011010011010010100101101100101101001101001001010110011011010010101100101101001001101101")]
        [DataRow("1234567890124", "1010110100101011001101101001010011010011001010100101011001101011010011001011010010101100100101101001101101")]
        [DataRow("1927947294788", "1010110100101001101011010100110010110100110100101001011011001011010011010010010101100110110101011001001101")]
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
