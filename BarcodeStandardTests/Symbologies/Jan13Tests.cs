using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeStandard;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class Jan13Tests
    {
        private readonly Barcode _barcode = new()
        {
            EncodedType = Type.Jan13,
        };

        [DataTestMethod]
        [DataRow("498000356216", "10100010110001001000110100011010100111010000101010100111010100001101100110011010100001101100101")]
        [DataRow("493456789012", "10100010110100001010001101100010000101001000101010100100011101001110010110011011011001011100101")]
        [DataRow("492794729478", "10100010110011011011101100010110011101001000101010110110011101001011100100010010010001110010101")]
        [DataRow("493123123123", "10100010110100001001100100100110100001011001101010110110010000101100110110110010000101001000101")]
        public void EncodeBarcode(string data, string expected)
        {
            _barcode.Encode(data);
            Assert.AreEqual(expected, _barcode.EncodedValue, $"{_barcode.EncodedType}");
        }

        [DataTestMethod]
        [DataRow("498000356216", "4980003562162")]
        [DataRow("4980003562162", "4980003562162")]
        [DataRow("493123123123", "4931231231238")]
        public void CalculateChecksum(string data, string expected)
        {
            _barcode.Encode(data);

            Assert.AreEqual(expected, _barcode.RawData);
        }
    }
}