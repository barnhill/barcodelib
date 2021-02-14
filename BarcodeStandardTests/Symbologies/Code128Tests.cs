using BarcodeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class Code128Tests
    {
        readonly Barcode barcode = new Barcode
        {
            EncodedType = TYPE.CODE128,
        };

        [DataRow("hello", "110100100001001100001010110010000110010100001100101000010001111010100010011001100011101011", null, "110100100001001100001010110010000110010100001100101000010001111010100010011001100011101011", null)]
        [DataRow("12094568792", "1101001110010110011100110010010001011101100010000100110100011110101110101111011001110010110111001001100011101011", "110100001001001110011011001110010100111011001110010110011001001110110111001001100111010011101001100111011011101110010110011001110010110110111101100011101011", "110100100001001110011011001110010100111011001110010110011001001110110111001001100111010011101001100111011011101110010110011001110010110111101101100011101011", "11010011100110011011001100100111010001011110111000101101111001010010101111000100010111101100011101011")]
        [DataRow("121234349090", "11010011100101100111001011001110010001011000100010110001101111011011011110110110110110001100011101011", "11010000100100111001101100111001010011100110110011100101100101110011001001110110010111001100100111011100101100100111011001110010110010011101100100010111101100011101011", "11010010000100111001101100111001010011100110110011100101100101110011001001110110010111001100100111011100101100100111011001110010110010011101100101111010001100011101011", "11010011100101100111001011001110010001011000100010110001101111011011011110110110110110001100011101011")]
        [DataRow("thisisalongerstring1212121212", "1101001000010011110100100110000101000011010010111100100100001101001011110010010010110000110010100001000111101011000010100100110100001011001000010010011110101111001001001111010010010011110100001101001100001010010011010000101110111101011001110010110011100101100111001011001110010110011100100011000101100011101011", null, "110100100001001111010010011000010100001101001011110010010000110100101111001001001011000011001010000100011110101100001010010011010000101100100001001001111010111100100100111101001001001111010000110100110000101001001101000010011100110110011100101001110011011001110010100111001101100111001010011100110110011100101001110011011001110010110001000101100011101011", null)]
        [DataRow("fighting!", "1101001000010110000100100001101001001101000010011000010100111101001000011010011000010100100110100001110101111011001101100100100011001100011101011", null, "11010010000101100001001000011010010011010000100110000101001111010010000110100110000101001001101000011001101100110111001001100011101011", null)]
        [DataRow("파이팅!", null, null, null, null)]
        [DataRow("ファイト！", null, null, null, null)]
        [DataTestMethod]
        public void EncodeBarcode(
            string data,
            string expectedAuto,
            string expectedA,
            string expectedB,
            string expectedC)
        {
            string getActualByType(
                TYPE type)
            {
                barcode.EncodedType = type;
                try
                {
                    return barcode.GenerateBarcode(data);
                }
                catch
                {
                    return null;
                }
            }
            Assert.AreEqual(expectedAuto, getActualByType(TYPE.CODE128), "Auto");
            Assert.AreEqual(expectedA, getActualByType(TYPE.CODE128A), "A");
            Assert.AreEqual(expectedB, getActualByType(TYPE.CODE128B), "B");
            Assert.AreEqual(expectedC, getActualByType(TYPE.CODE128C), "C");
        }

        /// <summary>
        ///   The output of this test can be used to build further tests as long as we can assume that the current behavior is good.
        /// </summary>
        [TestMethod]
        public void EncodeBarcode_GenerateDataRow()
        {
            foreach (var x in new[]
            {
                "hello",
                "12094568792",
                "121234349090",
                "thisisalongerstring1212121212",
                "fighting!",
                "파이팅!",
                "ファイト！",
            })
            {
                string represent(
                    string s)
                {
                    if (s == null) return "null";
                    return $"\"{s.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
                }
                string tryByType(
                    TYPE type)
                {
                    return represent(new Func<string>(() =>
                    {
                        try
                        {
                            barcode.EncodedType = type;
                            return barcode.GenerateBarcode(x);
                        }
                        catch
                        {
                            return null;
                        }
                    })());
                }
                Console.WriteLine($"        [DataRow({represent(x)}, {tryByType(TYPE.CODE128)}, {tryByType(TYPE.CODE128A)}, {tryByType(TYPE.CODE128B)}, {tryByType(TYPE.CODE128C)})]");
            }
        }
    }
}
