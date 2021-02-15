using System;
using System.Collections.Generic;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Code 128 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Code128 : BarcodeCommon, IBarcode
    {
        public static readonly char FNC1 = Convert.ToChar(200);
        public static readonly char FNC2 = Convert.ToChar(201);
        public static readonly char FNC3 = Convert.ToChar(202);
        public static readonly char FNC4 = Convert.ToChar(203);

        public enum TYPES : int { DYNAMIC, A, B, C };
        private readonly List<Code128Entry> C128_Code = new List<Code128Entry>();
        private readonly Dictionary<string, int> C128_CodeIndexByA = new Dictionary<string, int>();
        private readonly Dictionary<string, int> C128_CodeIndexByB = new Dictionary<string, int>();
        private readonly Dictionary<string, int> C128_CodeIndexByC = new Dictionary<string, int>();
        private List<string> _FormattedData = new List<string>();
        private List<string> _EncodedData = new List<string>();
        private Code128Entry? _startCharacter;
        private TYPES type = TYPES.DYNAMIC;

        /// <summary>
        /// Encodes data in Code128 format.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        public Code128(string input)
        {
            Raw_Data = input;
        }//Code128

        /// <summary>
        /// Encodes data in Code128 format.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        /// <param name="type">Type of encoding to lock to. (Code 128A, Code 128B, Code 128C)</param>
        public Code128(string input, TYPES type)
        {
            this.type = type;
            Raw_Data = input;
        }//Code128

        Code128Entry C128_ByA(string a) => C128_Code[C128_CodeIndexByA[a]];
        Code128Entry C128_ByB(string b) => C128_Code[C128_CodeIndexByB[b]];

        Code128Entry? C128_TryByLookup(Dictionary<string, int> lookup, string value) => lookup.TryGetValue(value, out var index) ? C128_Code[index] : new Code128Entry?();
        Code128Entry? C128_TryByA(string a) => C128_TryByLookup(C128_CodeIndexByA, a);
        Code128Entry? C128_TryByB(string b) => C128_TryByLookup(C128_CodeIndexByB, b);
        Code128Entry? C128_TryByC(string c) => C128_TryByLookup(C128_CodeIndexByC, c);

        private string Encode_Code128()
        {
            //initialize datastructure to hold encoding information
            init_Code128();

            return GetEncoding();
        }//Encode_Code128
        private void init_Code128()
        {
            //populate data
            void entry(uint? value, string a, string b, string c, string encoding)
            {
                C128_CodeIndexByA.Add(a, C128_Code.Count);
                C128_CodeIndexByB.Add(b, C128_Code.Count);
                C128_CodeIndexByC.Add(c, C128_Code.Count);
                C128_Code.Add(new Code128Entry(value, a, b, c, encoding));
            }
            entry(0, " ", " ", "00", "11011001100");
            entry(1, "!", "!", "01", "11001101100");
            entry(2, "\"", "\"", "02", "11001100110");
            entry(3, "#", "#", "03", "10010011000");
            entry(4, "$", "$", "04", "10010001100");
            entry(5, "%", "%", "05", "10001001100");
            entry(6, "&", "&", "06", "10011001000");
            entry(7, "'", "'", "07", "10011000100");
            entry(8, "(", "(", "08", "10001100100");
            entry(9, ")", ")", "09", "11001001000");
            entry(10, "*", "*", "10", "11001000100");
            entry(11, "+", "+", "11", "11000100100");
            entry(12, ",", ",", "12", "10110011100");
            entry(13, "-", "-", "13", "10011011100");
            entry(14, ".", ".", "14", "10011001110");
            entry(15, "/", "/", "15", "10111001100");
            entry(16, "0", "0", "16", "10011101100");
            entry(17, "1", "1", "17", "10011100110");
            entry(18, "2", "2", "18", "11001110010");
            entry(19, "3", "3", "19", "11001011100");
            entry(20, "4", "4", "20", "11001001110");
            entry(21, "5", "5", "21", "11011100100");
            entry(22, "6", "6", "22", "11001110100");
            entry(23, "7", "7", "23", "11101101110");
            entry(24, "8", "8", "24", "11101001100");
            entry(25, "9", "9", "25", "11100101100");
            entry(26, ":", ":", "26", "11100100110");
            entry(27, ";", ";", "27", "11101100100");
            entry(28, "<", "<", "28", "11100110100");
            entry(29, "=", "=", "29", "11100110010");
            entry(30, ">", ">", "30", "11011011000");
            entry(31, "?", "?", "31", "11011000110");
            entry(32, "@", "@", "32", "11000110110");
            entry(33, "A", "A", "33", "10100011000");
            entry(34, "B", "B", "34", "10001011000");
            entry(35, "C", "C", "35", "10001000110");
            entry(36, "D", "D", "36", "10110001000");
            entry(37, "E", "E", "37", "10001101000");
            entry(38, "F", "F", "38", "10001100010");
            entry(39, "G", "G", "39", "11010001000");
            entry(40, "H", "H", "40", "11000101000");
            entry(41, "I", "I", "41", "11000100010");
            entry(42, "J", "J", "42", "10110111000");
            entry(43, "K", "K", "43", "10110001110");
            entry(44, "L", "L", "44", "10001101110");
            entry(45, "M", "M", "45", "10111011000");
            entry(46, "N", "N", "46", "10111000110");
            entry(47, "O", "O", "47", "10001110110");
            entry(48, "P", "P", "48", "11101110110");
            entry(49, "Q", "Q", "49", "11010001110");
            entry(50, "R", "R", "50", "11000101110");
            entry(51, "S", "S", "51", "11011101000");
            entry(52, "T", "T", "52", "11011100010");
            entry(53, "U", "U", "53", "11011101110");
            entry(54, "V", "V", "54", "11101011000");
            entry(55, "W", "W", "55", "11101000110");
            entry(56, "X", "X", "56", "11100010110");
            entry(57, "Y", "Y", "57", "11101101000");
            entry(58, "Z", "Z", "58", "11101100010");
            entry(59, "[", "[", "59", "11100011010");
            entry(60, @"\", @"\", "60", "11101111010");
            entry(61, "]", "]", "61", "11001000010");
            entry(62, "^", "^", "62", "11110001010");
            entry(63, "_", "_", "63", "10100110000");
            entry(64, "\0", "`", "64", "10100001100");
            entry(65, Convert.ToChar(1).ToString(), "a", "65", "10010110000");
            entry(66, Convert.ToChar(2).ToString(), "b", "66", "10010000110");
            entry(67, Convert.ToChar(3).ToString(), "c", "67", "10000101100");
            entry(68, Convert.ToChar(4).ToString(), "d", "68", "10000100110");
            entry(69, Convert.ToChar(5).ToString(), "e", "69", "10110010000");
            entry(70, Convert.ToChar(6).ToString(), "f", "70", "10110000100");
            entry(71, Convert.ToChar(7).ToString(), "g", "71", "10011010000");
            entry(72, Convert.ToChar(8).ToString(), "h", "72", "10011000010");
            entry(73, Convert.ToChar(9).ToString(), "i", "73", "10000110100");
            entry(74, Convert.ToChar(10).ToString(), "j", "74", "10000110010");
            entry(75, Convert.ToChar(11).ToString(), "k", "75", "11000010010");
            entry(76, Convert.ToChar(12).ToString(), "l", "76", "11001010000");
            entry(77, Convert.ToChar(13).ToString(), "m", "77", "11110111010");
            entry(78, Convert.ToChar(14).ToString(), "n", "78", "11000010100");
            entry(79, Convert.ToChar(15).ToString(), "o", "79", "10001111010");
            entry(80, Convert.ToChar(16).ToString(), "p", "80", "10100111100");
            entry(81, Convert.ToChar(17).ToString(), "q", "81", "10010111100");
            entry(82, Convert.ToChar(18).ToString(), "r", "82", "10010011110");
            entry(83, Convert.ToChar(19).ToString(), "s", "83", "10111100100");
            entry(84, Convert.ToChar(20).ToString(), "t", "84", "10011110100");
            entry(85, Convert.ToChar(21).ToString(), "u", "85", "10011110010");
            entry(86, Convert.ToChar(22).ToString(), "v", "86", "11110100100");
            entry(87, Convert.ToChar(23).ToString(), "w", "87", "11110010100");
            entry(88, Convert.ToChar(24).ToString(), "x", "88", "11110010010");
            entry(89, Convert.ToChar(25).ToString(), "y", "89", "11011011110");
            entry(90, Convert.ToChar(26).ToString(), "z", "90", "11011110110");
            entry(91, Convert.ToChar(27).ToString(), "{", "91", "11110110110");
            entry(92, Convert.ToChar(28).ToString(), "|", "92", "10101111000");
            entry(93, Convert.ToChar(29).ToString(), "}", "93", "10100011110");
            entry(94, Convert.ToChar(30).ToString(), "~", "94", "10001011110");

            entry(95, Convert.ToChar(31).ToString(), Convert.ToChar(127).ToString(), "95", "10111101000");
            entry(96, "FNC3", "FNC3", "96", "10111100010");
            entry(97, "FNC2", "FNC2", "97", "11110101000");
            entry(98, "SHIFT", "SHIFT", "98", "11110100010");
            entry(99, "CODE_C", "CODE_C", "99", "10111011110");
            entry(100, "CODE_B", "FNC4", "CODE_B", "10111101110");
            entry(101, "FNC4", "CODE_A", "CODE_A", "11101011110");
            entry(102, "FNC1", "FNC1", "FNC1", "11110101110");
            entry(103, "START_A", "START_A", "START_A", "11010000100");
            entry(104, "START_B", "START_B", "START_B", "11010010000");
            entry(105, "START_C", "START_C", "START_C", "11010011100");
            entry(new uint?(), "STOP", "STOP", "STOP", "11000111010");

        }//init_Code128
        private List<Code128Entry> FindStartorCodeCharacter(string s, ref Func<Code128Entry, string> col)
        {
            var rows = new List<Code128Entry>();

            //if two chars are numbers (or FNC1) then START_C or CODE_C
            if (s.Length > 1 && (Char.IsNumber(s[0]) || s[0] == FNC1) && (Char.IsNumber(s[1]) || s[1] == FNC1))
            {
                if (!_startCharacter.HasValue)
                {
                    _startCharacter = C128_ByA("START_C");
                    rows.Add(_startCharacter.Value);
                }//if
                else
                    rows.Add(C128_ByA("CODE_C"));

                col = entry => entry.A;
            }//if
            else
            {
                var AFound = false;
                var BFound = false;
                foreach (var row in C128_Code)
                {
                    try
                    {
                        if (!AFound && s == row.A)
                        {
                            AFound = true;
                            col = entry => entry.B;

                            if (!_startCharacter.HasValue)
                            {
                                _startCharacter = C128_ByA("START_A");
                                rows.Add(_startCharacter.Value);
                            }//if
                            else
                            {
                                rows.Add(C128_ByB("CODE_A"));//first column is FNC4 so use B
                            }//else
                        }//if
                        else if (!BFound && s == row.B)
                        {
                            BFound = true;
                            col = entry => entry.A;

                            if (!_startCharacter.HasValue)
                            {
                                _startCharacter = C128_ByA("START_B");
                                rows.Add(_startCharacter.Value);
                            }//if
                            else
                                rows.Add(C128_ByA("CODE_B"));
                        }//else
                        else if (AFound && BFound)
                            break;
                    }//try
                    catch (Exception ex)
                    {
                        Error("EC128-1: " + ex.Message);
                    }//catch
                }//foreach                

                if (rows.Count <= 0)
                    Error("EC128-2: Could not determine start character.");
            }//else

            return rows;
        }
        private string CalculateCheckDigit()
        {
            uint checkSum = 0;

            for (uint i = 0; i < _FormattedData.Count; i++)
            {
                var s = _FormattedData[(int)i];

                //try to find value in the A column
                var rows = C128_TryByA(s);

                //try to find value in the B column
                if (!rows.HasValue)
                    rows = C128_TryByB(s);

                //try to find value in the C column
                if (!rows.HasValue)
                    rows = C128_TryByC(s);

                uint value = rows.Value.Value.Value;
                var addition = value * ((i == 0) ? 1 : i);
                checkSum += addition;
            }//for

            var remainder = checkSum % 103;
            var retRows = C128_Code[(int)remainder];
            return retRows.Encoding;
        }
        private void BreakUpDataForEncoding()
        {
            var temp = "";
            var tempRawData = Raw_Data;

            //breaking the raw data up for code A and code B will mess up the encoding
            switch (type)
            {
                case TYPES.A:
                case TYPES.B:
                    {
                        foreach (var c in Raw_Data)
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
                    }//if
                    else
                    {
                        temp += c;
                        _FormattedData.Add(temp);
                        temp = "";
                    }//else
                }//if
                else
                {
                    if (temp != "")
                    {
                        _FormattedData.Add(temp);
                        temp = "";
                    }//if
                    _FormattedData.Add(c.ToString());
                }//else
            }//foreach

            //if something is still in temp go ahead and push it onto the queue
            if (temp != "")
            {
                _FormattedData.Add(temp);
            }//if
        }
        private void InsertStartandCodeCharacters()
        {
            var CurrentCodeString = "";

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
            }//if
            else
            {
                try
                {
                    for (var i = 0; i < (_FormattedData.Count); i++)
                    {
                        Func<Code128Entry, string> col = entry => throw new InvalidOperationException($"Column not selected.");
                        var tempStartChars = FindStartorCodeCharacter(_FormattedData[i], ref col);

                        //check all the start characters and see if we need to stay with the same codeset or if a change of sets is required
                        var sameCodeSet = false;
                        foreach (var row in tempStartChars)
                        {
                            if (row.A.EndsWith(CurrentCodeString) || row.B.EndsWith(CurrentCodeString) || row.C.EndsWith(CurrentCodeString))
                            {
                                sameCodeSet = true;
                                break;
                            }//if
                        }//foreach

                        //only insert a new code char if starting a new codeset
                        //if (CurrentCodeString == "" || !tempStartChars[0][col].ToString().EndsWith(CurrentCodeString)) /* Removed because of bug */

                        if (CurrentCodeString == "" || !sameCodeSet)
                        {
                            var CurrentCodeSet = tempStartChars[0];

                            var found = false;
                            foreach (var possibleCol in new Func<Code128Entry, string>[] {
                                e => e.A,
                                e => e.B,
                                e => e.C,
                            })
                            {
                                try
                                {
                                    CurrentCodeString = possibleCol(CurrentCodeSet).Split(new char[] { '_' })[1];
                                    found = true;
                                    col = possibleCol;
                                    break;
                                }//try
                                catch
                                {
                                }//catch
                            }//foreach
                            if (!found)
                            {
                                Error("No start character found in CurrentCodeSet.");
                            }

                            _FormattedData.Insert(i++, col(CurrentCodeSet));
                        }//if

                    }//for
                }//try
                catch (Exception ex)
                {
                    Error("EC128-3: Could not insert start and code characters.\n Message: " + ex.Message);
                }//catch
            }//else
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
                var s1 = s.Replace("'", "''");
                Code128Entry? E_Row;

                //select encoding only for type selected
                switch (type)
                {
                    case TYPES.A:
                        E_Row = C128_TryByA(s1);
                        break;
                    case TYPES.B:
                        E_Row = C128_TryByB(s1);
                        break;
                    case TYPES.C:
                        E_Row = C128_TryByC(s1);
                        break;
                    case TYPES.DYNAMIC:
                        E_Row = C128_TryByA(s1);

                        if (!E_Row.HasValue)
                        {
                            E_Row = C128_TryByB(s1);

                            if (!E_Row.HasValue)
                            {
                                E_Row = C128_TryByC(s1);
                            }//if
                        }//if
                        break;
                    default:
                        E_Row = null;
                        break;
                }//switch              

                if (!E_Row.HasValue)
                    Error("EC128-5: Could not find encoding of a value( " + s1 + " ) in C128 type " + type.ToString());

                Encoded_Data += E_Row.Value.Encoding;
                _EncodedData.Add(E_Row.Value.Encoding);
            }//foreach

            //add the check digit
            string checkDigit = CalculateCheckDigit();
            Encoded_Data += checkDigit;
            _EncodedData.Add(checkDigit);

            //add the stop character
            var stop = C128_ByA("STOP");
            Encoded_Data += stop.Encoding;
            _EncodedData.Add(stop.Encoding);

            //add the termination bars
            Encoded_Data += "11";
            _EncodedData.Add("11");

            return Encoded_Data;
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_Code128();

        #endregion

        struct Code128Entry
        {
            public uint? Value { get; }
            public string A { get; }
            public string B { get; }
            public string C { get; }
            public string Encoding { get; }
            public Code128Entry(
                uint? value,
                string a,
                string b,
                string c,
                string encoding)
            {
                Value = value;
                A = a;
                B = b;
                C = c;
                Encoding = encoding;
            }
        }
    }//class
}//namespace
