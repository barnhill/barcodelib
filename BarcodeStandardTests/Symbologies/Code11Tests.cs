using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class Code11Tests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.Code11,
        };

        [DataTestMethod]
        [DataRow("123568902", "101100101101011010010110110010101101101010011010110100101101010101011010010110100101101011001")]
        [DataRow("102849-749", "1011001011010110101011010010110110100101011011011010101011010101001101011011011010101010110100110101011001")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
