using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class FimTests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.Fim,
        };

        [DataTestMethod]
        [DataRow("A", "10100000100000101")]
        [DataRow("B", "10001010001010001")]
        [DataRow("C", "10100010001000101")]
        [DataRow("D", "10101000100010101")]
        [DataRow("a", "10100000100000101")]
        [DataRow("b", "10001010001010001")]
        [DataRow("c", "10100010001000101")]
        [DataRow("d", "10101000100010101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
