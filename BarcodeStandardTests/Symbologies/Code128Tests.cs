using BarcodeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BarcodeStandardTests.Symbologies
{
    [TestClass]
    public class Code128Tests
    {
        readonly Barcode barcode = new Barcode
        {
            EncodedType = TYPE.CODE128,
        };

        [DataTestMethod]
        [DataRow("hello", "110100100001001100001010110010000110010100001100101000010001111010100010011001100011101011", null, "110100100001001100001010110010000110010100001100101000010001111010100010011001100011101011", null)]
        [DataRow("12094568792", "1101001110010110011100110010010001011101100010000100110100011110101110101111011001110010110111001001100011101011", "110100001001001110011011001110010100111011001110010110011001001110110111001001100111010011101001100111011011101110010110011001110010110110111101100011101011", "110100100001001110011011001110010100111011001110010110011001001110110111001001100111010011101001100111011011101110010110011001110010110111101101100011101011", "11010011100110011011001100100111010001011110111000101101111001010010101111000100010111101100011101011")]
        [DataRow("121234349090", "11010011100101100111001011001110010001011000100010110001101111011011011110110110110110001100011101011", "11010000100100111001101100111001010011100110110011100101100101110011001001110110010111001100100111011100101100100111011001110010110010011101100100010111101100011101011", "11010010000100111001101100111001010011100110110011100101100101110011001001110110010111001100100111011100101100100111011001110010110010011101100101111010001100011101011", "11010011100101100111001011001110010001011000100010110001101111011011011110110110110110001100011101011")]
        [DataRow("thisisalongerstring1212121212", "1101001000010011110100100110000101000011010010111100100100001101001011110010010010110000110010100001000111101011000010100100110100001011001000010010011110101111001001001111010010010011110100001101001100001010010011010000101110111101011001110010110011100101100111001011001110010110011100100011000101100011101011", null, "110100100001001111010010011000010100001101001011110010010000110100101111001001001011000011001010000100011110101100001010010011010000101100100001001001111010111100100100111101001001001111010000110100110000101001001101000010011100110110011100101001110011011001110010100111001101100111001010011100110110011100101001110011011001110010110001000101100011101011", null)]
        [DataRow("fighting!", "1101001000010110000100100001101001001101000010011000010100111101001000011010011000010100100110100001110101111011001101100100100011001100011101011", null, "11010010000101100001001000011010010011010000100110000101001111010010000110100110000101001001101000011001101100110111001001100011101011", null)]
        [DataRow("파이팅!", null, null, null, null)]
        [DataRow("ファイト！", null, null, null, null)]
        [DataRow("\u0012", "1101000010010010011110100100111101100011101011", "1101000010010010011110100100111101100011101011", null, null)]
        [DataRow("\u0014", "1101000010010011110100100111101001100011101011", "1101000010010011110100100111101001100011101011", null, null)]
        [DataRow("this\u0012is\u0014weird", "110100100001001111010010011000010100001101001011110010011101011110100100111101011110111010000110100101111001001110101111010011110100101111011101111001010010110010000100001101001001001111010000100110101100010001100011101011", null, null, null)]
        public void EncodeBarcode(
            string data,
            string expectedAuto,
            string expectedA,
            string expectedB,
            string expectedC)
        {
            void assertByType(
                TYPE type,
                string expected)
            {
                barcode.EncodedType = type;

                string actual = null;
                try
                {
                    actual = barcode.GenerateBarcode(data);
                }
                catch when (expected == null)
                {
                }
                Assert.AreEqual(expected, actual, $"{type}");
            }
            assertByType(TYPE.CODE128, expectedAuto);
            assertByType(TYPE.CODE128A, expectedA);
            assertByType(TYPE.CODE128B, expectedB);
            assertByType(TYPE.CODE128C, expectedC);
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
                // See issue #123.
                "\u0012",
                "\u0014",
                "this\u0012is\u0014weird",
            })
            {
                string represent(
                    string s)
                {
                    if (s == null) return "null";
                    return "\"" + string.Concat(s.Select(c =>
                    {
                        if (c == '\\') return "\\\\";
                        if (c == '"') return "\\\"";
                        if (c < 32) return $"\\u{(int)c:x04}";
                        return c.ToString();
                    })) + "\"";
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
