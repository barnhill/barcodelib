using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  Code 93 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Code93 : BarcodeCommon, IBarcode
    {
        private Dictionary<char, Code93CodeWord> codewordPatterns;
        private Dictionary<char, string> fullASCIITranslationTable;
        private Dictionary<int, char> codeWordToChar;

        /// <summary>
        /// Encodes with Code93.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        internal Code93(string input)
        {
            RawData = input;
        }

        /// <summary>
        /// Encode the raw data using the Code 93 algorithm.
        /// </summary>
        private string Encode_Code93()
        {
            InitCodeWordPatterns();
            InitFullASCIITranslationTable();

            var formattedData = MapFullASCII(RawData);
            formattedData = "*" + formattedData + CalculateChecksum(formattedData) + "*";

            var sb = new StringBuilder(9 * (formattedData.Length + 2));

            foreach (var c in formattedData)
            {
                sb.Append(codewordPatterns[c].Pattern);
            }

            // Termination bar
            sb.Append("1");

            codewordPatterns.Clear();
            fullASCIITranslationTable.Clear();
            codeWordToChar.Clear();

            return sb.ToString();
        }

        private string MapFullASCII(string toBeMapped)
        {
            // A character encoded in full ASCII mode in Code 39 will potentially generate two characters.
            var sb = new StringBuilder(toBeMapped.Length * 2);

            // Used for reporting the position of a character that can't be encoded
            int pos = 0;

            try
            {
                // Iterate over each character in the input and possibly translate
                // it to the full ASCII representation.
                for (pos = 0; pos < toBeMapped.Length; pos++)
                {
                    sb.Append(fullASCIITranslationTable[toBeMapped[pos]]);
                }
            }
            catch (KeyNotFoundException e)
            {
                // A character that can't be encoded was supplied as input
                throw new Exception($"Invalid character in input string at index {pos} ASCII code [{(int)toBeMapped[pos]}]: {e.Message}");
            }

            return sb.ToString();
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_Code93();

        #endregion

        private string CalculateChecksum(string code)
        {
            (var sumC, var sumK) = CalculateChecksums(code);

            return $"{codeWordToChar[sumC]}{codeWordToChar[sumK]}";
        }

        private (int CheckSumC, int CheckSumK) CalculateChecksums(string code)
        {
            int sumC = 0, sumK = 0, weightC = 1, weightK = 2;  // weightK at 2 because K checksum considers C as the first weight

            for (int i = code.Length - 1; i >= 0; i--)
            {
                var codeWordValue = codewordPatterns[code[i]].CodeWordValue;

                sumC += codeWordValue * weightC;
                sumK += codeWordValue * weightK;

                weightC = (weightC == 20) ? 1 : weightC + 1;
                weightK = (weightK == 15) ? 1 : weightK + 1;
            }

            sumK += (sumC % 47);  // Add the weight of C checksum for K calculation

            return (sumC % 47, sumK % 47);
        }

        private void InitCodeWordPatterns()
        {
            // Encoding character as bBsS where b or B means bar and s or S means space.
            // Upper case means it is a wide element. Lower case is a narrow element.
            codewordPatterns = new Dictionary<char, Code93CodeWord>
            {
                { '0', new Code93CodeWord() { Pattern = "100010100", CodeWordValue =  0 }},
                { '1', new Code93CodeWord() { Pattern = "101001000", CodeWordValue =  1 }},
                { '2', new Code93CodeWord() { Pattern = "101000100", CodeWordValue =  2 }},
                { '3', new Code93CodeWord() { Pattern = "101000010", CodeWordValue =  3 }},
                { '4', new Code93CodeWord() { Pattern = "100101000", CodeWordValue =  4 }},
                { '5', new Code93CodeWord() { Pattern = "100100100", CodeWordValue =  5 }},
                { '6', new Code93CodeWord() { Pattern = "100100010", CodeWordValue =  6 }},
                { '7', new Code93CodeWord() { Pattern = "101010000", CodeWordValue =  7 }},
                { '8', new Code93CodeWord() { Pattern = "100010010", CodeWordValue =  8 }},
                { '9', new Code93CodeWord() { Pattern = "100001010", CodeWordValue =  9 }},
                { 'A', new Code93CodeWord() { Pattern = "110101000", CodeWordValue = 10 }},
                { 'B', new Code93CodeWord() { Pattern = "110100100", CodeWordValue = 11 }},
                { 'C', new Code93CodeWord() { Pattern = "110100010", CodeWordValue = 12 }},
                { 'D', new Code93CodeWord() { Pattern = "110010100", CodeWordValue = 13 }},
                { 'E', new Code93CodeWord() { Pattern = "110010010", CodeWordValue = 14 }},
                { 'F', new Code93CodeWord() { Pattern = "110001010", CodeWordValue = 15 }},
                { 'G', new Code93CodeWord() { Pattern = "101101000", CodeWordValue = 16 }},
                { 'H', new Code93CodeWord() { Pattern = "101100100", CodeWordValue = 17 }},
                { 'I', new Code93CodeWord() { Pattern = "101100010", CodeWordValue = 18 }},
                { 'J', new Code93CodeWord() { Pattern = "100110100", CodeWordValue = 19 }},
                { 'K', new Code93CodeWord() { Pattern = "100011010", CodeWordValue = 20 }},
                { 'L', new Code93CodeWord() { Pattern = "101011000", CodeWordValue = 21 }},
                { 'M', new Code93CodeWord() { Pattern = "101001100", CodeWordValue = 22 }},
                { 'N', new Code93CodeWord() { Pattern = "101000110", CodeWordValue = 23 }},
                { 'O', new Code93CodeWord() { Pattern = "100101100", CodeWordValue = 24 }},
                { 'P', new Code93CodeWord() { Pattern = "100010110", CodeWordValue = 25 }},
                { 'Q', new Code93CodeWord() { Pattern = "110110100", CodeWordValue = 26 }},
                { 'R', new Code93CodeWord() { Pattern = "110110010", CodeWordValue = 27 }},
                { 'S', new Code93CodeWord() { Pattern = "110101100", CodeWordValue = 28 }},
                { 'T', new Code93CodeWord() { Pattern = "110100110", CodeWordValue = 29 }},
                { 'U', new Code93CodeWord() { Pattern = "110010110", CodeWordValue = 30 }},
                { 'V', new Code93CodeWord() { Pattern = "110011010", CodeWordValue = 31 }},
                { 'W', new Code93CodeWord() { Pattern = "101101100", CodeWordValue = 32 }},
                { 'X', new Code93CodeWord() { Pattern = "101100110", CodeWordValue = 33 }},
                { 'Y', new Code93CodeWord() { Pattern = "100110110", CodeWordValue = 34 }},
                { 'Z', new Code93CodeWord() { Pattern = "100111010", CodeWordValue = 35 }},
                { '-', new Code93CodeWord() { Pattern = "100101110", CodeWordValue = 36 }},
                { '.', new Code93CodeWord() { Pattern = "111010100", CodeWordValue = 37 }},
                { ' ', new Code93CodeWord() { Pattern = "111010010", CodeWordValue = 38 }},
                { '$', new Code93CodeWord() { Pattern = "111001010", CodeWordValue = 39 }},
                { '/', new Code93CodeWord() { Pattern = "101101110", CodeWordValue = 40 }},
                { '+', new Code93CodeWord() { Pattern = "101110110", CodeWordValue = 41 }},
                { '%', new Code93CodeWord() { Pattern = "110101110", CodeWordValue = 42 }},
                { '①', new Code93CodeWord() { Pattern = "100100110", CodeWordValue = 43 }}, // ($)
                { '②', new Code93CodeWord() { Pattern = "111011010", CodeWordValue = 44 }}, // (%)
                { '③', new Code93CodeWord() { Pattern = "111010110", CodeWordValue = 45 }}, // (/)
                { '④', new Code93CodeWord() { Pattern = "100110010", CodeWordValue = 46 }}, // (+)
                { '*', new Code93CodeWord() { Pattern = "101011110", CodeWordValue = -1 }}, // Start/Stop *
                { '⑤', new Code93CodeWord() { Pattern = "101111010", CodeWordValue = -2 }}, // (Reverse stop)
            };

            codeWordToChar = InitCodeWordToChar();
        }

        private Dictionary<int, char> InitCodeWordToChar()
        {
            var dict = new Dictionary<int, char>();

            foreach (KeyValuePair<char, Code93CodeWord> kvp in codewordPatterns)
            {
                dict.Add(kvp.Value.CodeWordValue, kvp.Key);
            }

            return dict;
        }

        private void InitFullASCIITranslationTable()
        {
            #pragma warning disable format
            fullASCIITranslationTable = new Dictionary<char, string>()
            {
                // Char     Representation Control/char ASCII
                { '\u0000', "②U"  },   // NUL          0
                { '\u0001', "①A"  },   // SOH          1
                { '\u0002', "①B"  },   // STX          2
                { '\u0003', "①C"  },   // ETX          3
                { '\u0004', "①D"  },   // EOT          4
                { '\u0005', "①E"  },   // ENQ          5
                { '\u0006', "①F"  },   // ACK          6
                { '\u0007', "①G"  },   // BEL          7
                { '\u0008', "①H"  },   // BS           8
                { '\u0009', "①I"  },   // HT           9
                { '\u000a', "①J"  },   // LF           10
                { '\u000b', "①K"  },   // VT           11
                { '\u000c', "①L"  },   // FF           12
                { '\u000d', "①M"  },   // CR           13
                { '\u000e', "①N"  },   // SO           14
                { '\u000f', "①O"  },   // SI           15
                { '\u0010', "①P"  },   // DLE          16
                { '\u0011', "①Q"  },   // DC1          17
                { '\u0012', "①R"  },   // DC2          18
                { '\u0013', "①S"  },   // DC3          19
                { '\u0014', "①T"  },   // DC4          20
                { '\u0015', "①U"  },   // NAK          21
                { '\u0016', "①V"  },   // SYN          22
                { '\u0017', "①W"  },   // ETB          23
                { '\u0018', "①X"  },   // CAN          24
                { '\u0019', "①Y"  },   // EM           25
                { '\u001a', "①Z"  },   // SUB          26
                { '\u001b', "②A"  },   // ESC          27
                { '\u001c', "②B"  },   // FS           28
                { '\u001d', "②C"  },   // GS           29
                { '\u001e', "②D"  },   // RS           30
                { '\u001f', "②E"  },   // US           31
                { ' ',      " "   },    //              32
                { '!',      "③A"  },   // !            33
                { '"',      "③B"  },   // "            34
                { '#',      "③C"  },   // #            35
                { '$',      "$"  },     // $            36
                { '%',      "%"  },     // %            37
                { '&',      "③F"  },   // &            38
                { '\'',     "③G"  },   // \            39
                { '(',      "③H"  },   // (            40
                { ')',      "③I"  },   // )            41
                { '*',      "③J"  },   // *            42
                { '+',      "+"  },     // +            43
                { ',',      "③L"  },   // ,            44
                { '-',      "-"   },    // -            45
                { '.',      "."   },    // .            46
                { '/',      "/"  },     // /            47
                { '0',      "0"   },    // 0            48
                { '1',      "1"   },    // 1            49
                { '2',      "2"   },    // 2            50
                { '3',      "3"   },    // 3            51
                { '4',      "4"   },    // 4            52
                { '5',      "5"   },    // 5            53
                { '6',      "6"   },    // 6            54
                { '7',      "7"   },    // 7            55
                { '8',      "8"   },    // 8            56
                { '9',      "9"   },    // 9            57
                { ':',      "③Z"  },   // :            58
                { ';',      "②F"  },   // ;            59
                { '<',      "②G"  },   // <            60
                { '=',      "②H"  },   // =            61
                { '>',      "②I"  },   // >            62
                { '?',      "②J"  },   // ?            63
                { '@',      "②V"  },   // @            64
                { 'A',      "A"   },    // A            65
                { 'B',      "B"   },    // B            66
                { 'C',      "C"   },    // C            67
                { 'D',      "D"   },    // D            68
                { 'E',      "E"   },    // E            69
                { 'F',      "F"   },    // F            70
                { 'G',      "G"   },    // G            71
                { 'H',      "H"   },    // H            72
                { 'I',      "I"   },    // I            73
                { 'J',      "J"   },    // J            74
                { 'K',      "K"   },    // K            75
                { 'L',      "L"   },    // L            76
                { 'M',      "M"   },    // M            77
                { 'N',      "N"   },    // N            78
                { 'O',      "O"   },    // O            79
                { 'P',      "P"   },    // P            80
                { 'Q',      "Q"   },    // Q            81
                { 'R',      "R"   },    // R            82
                { 'S',      "S"   },    // S            83
                { 'T',      "T"   },    // T            84
                { 'U',      "U"   },    // U            85
                { 'V',      "V"   },    // V            86
                { 'W',      "W"   },    // W            87
                { 'X',      "X"   },    // X            88
                { 'Y',      "Y"   },    // Y            89
                { 'Z',      "Z"   },    // Z            90
                { '[',      "②K"  },   // [            91
                { '\\',     "②L"  },   // \            92
                { ']',      "②M"  },   // ]            93
                { '^',      "②N"  },   // ^            94
                { '_',      "②O"  },   // _            95
                { '`',      "②W"  },   // `            96
                { 'a',      "④A"  },   // a            97
                { 'b',      "④B"  },   // b            98
                { 'c',      "④C"  },   // c            99
                { 'd',      "④D"  },   // d            100
                { 'e',      "④E"  },   // e            101
                { 'f',      "④F"  },   // f            102
                { 'g',      "④G"  },   // g            103
                { 'h',      "④H"  },   // h            104
                { 'i',      "④I"  },   // i            105
                { 'j',      "④J"  },   // j            106
                { 'k',      "④K"  },   // k            107
                { 'l',      "④L"  },   // l            108
                { 'm',      "④M"  },   // m            109
                { 'n',      "④N"  },   // n            110
                { 'o',      "④O"  },   // o            111
                { 'p',      "④P"  },   // p            112
                { 'q',      "④Q"  },   // q            113
                { 's',      "④S"  },   // s            114
                { 't',      "④T"  },   // t            115
                { 'r',      "④R"  },   // r            116
                { 'u',      "④U"  },   // u            117
                { 'v',      "④V"  },   // v            118
                { 'w',      "④W"  },   // w            119
                { 'x',      "④X"  },   // x            120
                { 'y',      "④Y"  },   // y            121
                { 'z',      "④Z"  },   // z            122
                { '{',      "②P"  },   // {            123
                { '|',      "②Q"  },   // |            124
                { '}',      "②R"  },   // }            125
                { '~',      "②S"  },   // ~            126
                { '\u007f', "②T"  },   // DEL          127
                { '①',      "①"  },   // ($)          n/a
                { '②',      "②"  },   // (%)          n/a
                { '③',      "③"  },   // (/)          n/a
                { '④',      "④"  },   // (+)          n/a
                { '⑤',      "⑤"  },   // Reverse stop n/a
            };
            #pragma warning restore format
        }

    }

    class Code93CodeWord
    {
        public string Pattern { get; set; }
        public int CodeWordValue { get; set; }
    }
}
