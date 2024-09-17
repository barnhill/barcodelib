using BarcodeStandard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class UpcETests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.UpcE,
        };

        [DataTestMethod]
        [DataRow("038000000216", "101010000101101110100111001001100110010100111010101")]
        [DataRow("654321", "101000010101100010011101011110100110110011001010101")]
        [DataRow("06543214", "101000010101100010011101011110100110110011001010101")]
        [DataRow("065100004327", "101000010101100010011101011110100110110011001010101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }
    }
}
