using System;
using System.Collections.Generic;

namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  Code 128 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Code128 : BarcodeCommon, IBarcode
    {
        public static readonly char FNC1 = Convert.ToChar(200);
        public static readonly char FNC2 = Convert.ToChar(201);
        public static readonly char FNC3 = Convert.ToChar(202);
        public static readonly char FNC4 = Convert.ToChar(203);

        public enum TYPES : int { DYNAMIC, A, B, C };
        private readonly List<string> C128_Code = [];
        private readonly Dictionary<string, int> C128_CodeIndexByA = [];
        private readonly Dictionary<string, int> C128_CodeIndexByB = [];
        private readonly Dictionary<string, int> C128_CodeIndexByC = [];
        private readonly List<string> _FormattedData = [];
        private readonly List<string> _EncodedData = [];
        private int? _startCharacterIndex;
        private readonly TYPES type = TYPES.DYNAMIC;

        /// <summary>
        /// Encodes data in Code128 format.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        internal Code128(string input)
        {
            RawData = input;
        }

        /// <summary>
        /// Encodes data in Code128 format.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        /// <param name="type">Type of encoding to lock to. (Code 128A, Code 128B, Code 128C)</param>
        internal Code128(string input, TYPES type)
        {
            this.type = type;
            RawData = input;
        }

        string C128_ByA(string a) => C128_Code[C128_CodeIndexByA[a]];

        string C128_TryByLookup(Dictionary<string, int> lookup, string value) => lookup.TryGetValue(value, out var index) ? C128_Code[index] : null;
        string C128_TryByA(string a) => C128_TryByLookup(C128_CodeIndexByA, a);
        string C128_TryByB(string b) => C128_TryByLookup(C128_CodeIndexByB, b);
        string C128_TryByC(string c) => C128_TryByLookup(C128_CodeIndexByC, c);

        private string Encode_Code128()
        {
            //initialize datastructure to hold encoding information
            Init_Code128();

            return GetEncoding();
        }
        private void Init_Code128()
        {
            //populate data
            void entry(string a, string b, string c, string encoding)
            {
                C128_CodeIndexByA.Add(a, C128_Code.Count);
                C128_CodeIndexByB.Add(b, C128_Code.Count);
                C128_CodeIndexByC.Add(c, C128_Code.Count);
                C128_Code.Add(encoding);
            }
            entry(" ", " ", "00", "11011001100");
            entry("!", "!", "01", "11001101100");
            entry("\"", "\"", "02", "11001100110");
            entry("#", "#", "03", "10010011000");
            entry("$", "$", "04", "10010001100");
            entry("%", "%", "05", "10001001100");
            entry("&", "&", "06", "10011001000");
            entry("'", "'", "07", "10011000100");
            entry("(", "(", "08", "10001100100");
            entry(")", ")", "09", "11001001000");
            entry("*", "*", "10", "11001000100");
            entry("+", "+", "11", "11000100100");
            entry(",", ",", "12", "10110011100");
            entry("-", "-", "13", "10011011100");
            entry(".", ".", "14", "10011001110");
            entry("/", "/", "15", "10111001100");
            entry("0", "0", "16", "10011101100");
            entry("1", "1", "17", "10011100110");
            entry("2", "2", "18", "11001110010");
            entry("3", "3", "19", "11001011100");
            entry("4", "4", "20", "11001001110");
            entry("5", "5", "21", "11011100100");
            entry("6", "6", "22", "11001110100");
            entry("7", "7", "23", "11101101110");
            entry("8", "8", "24", "11101001100");
            entry("9", "9", "25", "11100101100");
            entry(":", ":", "26", "11100100110");
            entry(";", ";", "27", "11101100100");
            entry("<", "<", "28", "11100110100");
            entry("=", "=", "29", "11100110010");
            entry(">", ">", "30", "11011011000");
            entry("?", "?", "31", "11011000110");
            entry("@", "@", "32", "11000110110");
            entry("A", "A", "33", "10100011000");
            entry("B", "B", "34", "10001011000");
            entry("C", "C", "35", "10001000110");
            entry("D", "D", "36", "10110001000");
            entry("E", "E", "37", "10001101000");
            entry("F", "F", "38", "10001100010");
            entry("G", "G", "39", "11010001000");
            entry("H", "H", "40", "11000101000");
            entry("I", "I", "41", "11000100010");
            entry("J", "J", "42", "10110111000");
            entry("K", "K", "43", "10110001110");
            entry("L", "L", "44", "10001101110");
            entry("M", "M", "45", "10111011000");
            entry("N", "N", "46", "10111000110");
            entry("O", "O", "47", "10001110110");
            entry("P", "P", "48", "11101110110");
            entry("Q", "Q", "49", "11010001110");
            entry("R", "R", "50", "11000101110");
            entry("S", "S", "51", "11011101000");
            entry("T", "T", "52", "11011100010");
            entry("U", "U", "53", "11011101110");
            entry("V", "V", "54", "11101011000");
            entry("W", "W", "55", "11101000110");
            entry("X", "X", "56", "11100010110");
            entry("Y", "Y", "57", "11101101000");
            entry("Z", "Z", "58", "11101100010");
            entry("[", "[", "59", "11100011010");
            entry(@"\", @"\", "60", "11101111010");
            entry("]", "]", "61", "11001000010");
            entry("^", "^", "62", "11110001010");
            entry("_", "_", "63", "10100110000");
            entry("\0", "`", "64", "10100001100");
            entry(Convert.ToChar(1).ToString(), "a", "65", "10010110000");
            entry(Convert.ToChar(2).ToString(), "b", "66", "10010000110");
            entry(Convert.ToChar(3).ToString(), "c", "67", "10000101100");
            entry(Convert.ToChar(4).ToString(), "d", "68", "10000100110");
            entry(Convert.ToChar(5).ToString(), "e", "69", "10110010000");
            entry(Convert.ToChar(6).ToString(), "f", "70", "10110000100");
            entry(Convert.ToChar(7).ToString(), "g", "71", "10011010000");
            entry(Convert.ToChar(8).ToString(), "h", "72", "10011000010");
            entry(Convert.ToChar(9).ToString(), "i", "73", "10000110100");
            entry(Convert.ToChar(10).ToString(), "j", "74", "10000110010");
            entry(Convert.ToChar(11).ToString(), "k", "75", "11000010010");
            entry(Convert.ToChar(12).ToString(), "l", "76", "11001010000");
            entry(Convert.ToChar(13).ToString(), "m", "77", "11110111010");
            entry(Convert.ToChar(14).ToString(), "n", "78", "11000010100");
            entry(Convert.ToChar(15).ToString(), "o", "79", "10001111010");
            entry(Convert.ToChar(16).ToString(), "p", "80", "10100111100");
            entry(Convert.ToChar(17).ToString(), "q", "81", "10010111100");
            entry(Convert.ToChar(18).ToString(), "r", "82", "10010011110");
            entry(Convert.ToChar(19).ToString(), "s", "83", "10111100100");
            entry(Convert.ToChar(20).ToString(), "t", "84", "10011110100");
            entry(Convert.ToChar(21).ToString(), "u", "85", "10011110010");
            entry(Convert.ToChar(22).ToString(), "v", "86", "11110100100");
            entry(Convert.ToChar(23).ToString(), "w", "87", "11110010100");
            entry(Convert.ToChar(24).ToString(), "x", "88", "11110010010");
            entry(Convert.ToChar(25).ToString(), "y", "89", "11011011110");
            entry(Convert.ToChar(26).ToString(), "z", "90", "11011110110");
            entry(Convert.ToChar(27).ToString(), "{", "91", "11110110110");
            entry(Convert.ToChar(28).ToString(), "|", "92", "10101111000");
            entry(Convert.ToChar(29).ToString(), "}", "93", "10100011110");
            entry(Convert.ToChar(30).ToString(), "~", "94", "10001011110");

            entry(Convert.ToChar(31).ToString(), Convert.ToChar(127).ToString(), "95", "10111101000");
            entry("FNC3", "FNC3", "96", "10111100010");
            entry("FNC2", "FNC2", "97", "11110101000");
            entry("SHIFT", "SHIFT", "98", "11110100010");
            entry("CODE_C", "CODE_C", "99", "10111011110");
            entry("CODE_B", "FNC4", "CODE_B", "10111101110");
            entry("FNC4", "CODE_A", "CODE_A", "11101011110");
            entry($"{FNC1}", $"{FNC1}", $"{FNC1}", "11110101110");
            entry("START_A", "START_A", "START_A", "11010000100");
            entry("START_B", "START_B", "START_B", "11010010000");
            entry("START_C", "START_C", "START_C", "11010011100");
            entry("STOP", "STOP", "STOP", "11000111010");

        }//init_Code128
        private List<int> FindStartorCodeCharacter(string s)
        {
            var rows = new List<int>();

            // if two chars are numbers (or FNC1) then START_C or CODE_C
            if (s.Length > 1 && (Char.IsNumber(s[0]) || s[0] == FNC1) && (Char.IsNumber(s[1]) || s[1] == FNC1))
            {
                if (!_startCharacterIndex.HasValue)
                {
                    _startCharacterIndex = C128_CodeIndexByA["START_C"];
                    rows.Add(_startCharacterIndex.Value);
                }
                else
                    rows.Add(C128_CodeIndexByA["CODE_C"]);
            }
            else
            {
                try
                {
                    var AFound = C128_CodeIndexByA.TryGetValue(s, out var aIndex);
                    if (AFound)
                    {
                        if (!_startCharacterIndex.HasValue)
                        {
                            _startCharacterIndex = C128_CodeIndexByA["START_A"];
                            rows.Add(_startCharacterIndex.Value);
                        }
                        else
                        {
                            rows.Add(C128_CodeIndexByB["CODE_A"]);//first column is FNC4 so use B
                        }
                    }
                    var BFound = C128_CodeIndexByB.TryGetValue(s, out var bIndex) && (!AFound || bIndex != aIndex);
                    if (BFound)
                    {
                        if (!_startCharacterIndex.HasValue)
                        {
                            _startCharacterIndex = C128_CodeIndexByA["START_B"];
                            rows.Add(_startCharacterIndex.Value);
                        }
                        else
                        {
                            rows.Add(C128_CodeIndexByA["CODE_B"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error("EC128-1: " + ex.Message);
                }

                if (rows.Count <= 0)
                    Error("EC128-2: Could not determine start character.");
            }

            return rows;
        }
        private string CalculateCheckDigit()
        {
            uint checkSum = 0;

            for (uint i = 0; i < _FormattedData.Count; i++)
            {
                var s = _FormattedData[(int)i];

                 // try to find value in the A column
                var value = C128_CodeIndexByA.TryGetValue(s, out var index)
                // try to find value in the B column
                    || C128_CodeIndexByB.TryGetValue(s, out index)
                // try to find value in the C column
                    || C128_CodeIndexByC.TryGetValue(s, out index) ? (uint)index : throw new InvalidOperationException($"Unable to find character “{s}”");

                var addition = value * ((i == 0) ? 1 : i);
                checkSum += addition;
            }

            var remainder = checkSum % 103;
            return C128_Code[(int)remainder];
        }
        private void BreakUpDataForEncoding()
        {
            var temp = "";
            var tempRawData = RawData;

            //breaking the raw data up for code A and code B will mess up the encoding
            switch (type)
            {
                case TYPES.A:
                case TYPES.B:
                    {
                        foreach (var c in RawData)
                            _FormattedData.Add(c.ToString());
                        return;
                    }

                case TYPES.C:
                    {
                        var indexOfFirstNumeric = -1;
                        var numericCount = 0;
                        for (var x = 0; x < RawData.Length; x++)
                        {
                            var c = RawData[x];
                            if (Char.IsNumber(c))
                            {
                                numericCount++;
                                if (indexOfFirstNumeric == -1)
                                {
                                    indexOfFirstNumeric = x;
                                }
                            }
                            else if (c != FNC1)
                            {
                                Error("EC128-6: Only numeric values can be encoded with C128-C (Invalid char at position " + x + ").");
                            }
                        }

                        //CODE C: adds a 0 to the front of the Raw_Data if the length is not divisible by 2
                        if (numericCount % 2 == 1)
                            tempRawData = tempRawData.Insert(indexOfFirstNumeric, "0");
                        break;
                    }
            }

            foreach (var c in tempRawData)
            {
                if (Char.IsNumber(c))
                {
                    if (temp == "")
                    {
                        temp += c;
                    }
                    else
                    {
                        temp += c;
                        _FormattedData.Add(temp);
                        temp = "";
                    }
                }
                else
                {
                    if (temp != "")
                    {
                        _FormattedData.Add(temp);
                        temp = "";
                    }
                    _FormattedData.Add(c.ToString());
                }
            }

            // if something is still in temp go ahead and push it onto the queue
            if (temp != "")
            {
                _FormattedData.Add(temp);
            }
        }
        private void InsertStartandCodeCharacters()
        {
            if (type != TYPES.DYNAMIC)
            {
                switch (type)
                {
                    case TYPES.A:
                        _FormattedData.Insert(0, "START_A");
                        break;
                    case TYPES.B:
                        _FormattedData.Insert(0, "START_B");
                        break;
                    case TYPES.C:
                        _FormattedData.Insert(0, "START_C");
                        break;
                    default:
                        Error("EC128-4: Unknown start type in fixed type encoding.");
                        break;
                }
            }
            else
            {
                try
                {
                    Dictionary<string, int> col = null;
                    var currentStartCode = "";

                    for (var i = 0; i < _FormattedData.Count; i++)
                    {
                        var currentElement = _FormattedData[i];
                        var tempStartChars = FindStartorCodeCharacter(currentElement);

                        //check all the start characters and see if we need to stay with the same codeset or if a change of sets is required
                        var sameCodeSet = false;
                        foreach (var row in tempStartChars)
                        {
                            if ((C128_CodeIndexByA.ContainsKey(currentElement) && currentStartCode == "START_A")
                                || (C128_CodeIndexByB.ContainsKey(currentElement) && currentStartCode == "START_B")
                                || (C128_CodeIndexByC.ContainsKey(currentElement) && currentStartCode == "START_C"))
                            {
                                sameCodeSet = true;
                                break;
                            }
                        }

                        //only insert a new code char if starting a new codeset
                        // if (CurrentCodeString == "" || !tempStartChars[0][col].ToString().EndsWith(CurrentCodeString)) /* Removed because of bug */

                        if (col == null || !sameCodeSet)
                        {
                            var CurrentCodeSet = tempStartChars[0];

                            foreach (var (start, code, nextCol) in new[] {
                                ("START_A", "CODE_A", C128_CodeIndexByA),
                                ("START_B", "CODE_B", C128_CodeIndexByB),
                                ("START_C", "CODE_C", C128_CodeIndexByC),
                            })
                            {
                                if (col == null)
                                {
                                    // We still need to write the start char and establish the current code.
                                    if (CurrentCodeSet == C128_CodeIndexByA[start])
                                    {
                                        col = nextCol;
                                        currentStartCode = start;
                                        _FormattedData.Insert(i++, start);
                                        break;
                                    }
                                }
                                else
                                {
                                    // We need to switch codes.
                                    if (col != nextCol && CurrentCodeSet == col[code])
                                    {
                                        col = nextCol;
                                        currentStartCode = start;
                                        _FormattedData.Insert(i++, code);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Error("EC128-3: Could not insert start and code characters.\n Message: " + ex.Message);
                }
            }
        }
        private string GetEncoding()
        {
            //break up data for encoding
            BreakUpDataForEncoding();

            //insert the start characters
            InsertStartandCodeCharacters();

            var Encoded_Data = "";
            foreach (var s in _FormattedData)
            {
                //handle exception with apostrophes in select statements
                string E_Row;

                //select encoding only for type selected
                switch (type)
                {
                    case TYPES.A:
                        E_Row = C128_TryByA(s);
                        break;
                    case TYPES.B:
                        E_Row = C128_TryByB(s);
                        break;
                    case TYPES.C:
                        E_Row = C128_TryByC(s);
                        break;
                    case TYPES.DYNAMIC:
                        E_Row = C128_TryByA(s);

                        if (E_Row == null)
                        {
                            E_Row = C128_TryByB(s);

                            E_Row ??= C128_TryByC(s);
                        }
                        break;
                    default:
                        E_Row = null;
                        break;
                }              

                if (E_Row == null)
                    Error("EC128-5: Could not find encoding of a value( " + s + " ) in C128 type " + type.ToString());

                Encoded_Data += E_Row;
                _EncodedData.Add(E_Row);
            }

            //add the check digit
            string checkDigit = CalculateCheckDigit();
            Encoded_Data += checkDigit;
            _EncodedData.Add(checkDigit);

            //add the stop character
            var stop = C128_ByA("STOP");
            Encoded_Data += stop;
            _EncodedData.Add(stop);

            //add the termination bars
            Encoded_Data += "11";
            _EncodedData.Add("11");

            return Encoded_Data;
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_Code128();

        #endregion
    }
}
