using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    public class Code128CustomBuilder
    {
        public enum Code128Type { A, B, C };
        private string Code128String { get; set; }

        public static Code128CustomBuilder Start(Code128Type type, string barcodePart)
        {
            return new Code128CustomBuilder()
            {
                Code128String = String.Format("{0}{1}{2}", char.ConvertFromUtf32(140), type.ToString(), barcodePart)
            };
        }

        public Code128CustomBuilder AddPart(Code128Type type, string barcodePart)
        {
            return new Code128CustomBuilder()
            {
                Code128String =
                    string.Format("{0}{1}{2}{3}", Code128String, char.ConvertFromUtf32(140), type.ToString(), barcodePart)
            };
        }

        public string GetBarcodeString()
        {
            return Code128String;
        }
    }
}
