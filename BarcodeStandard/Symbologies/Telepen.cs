using System;
using System.Collections;
using BarcodeStandard;

namespace BarcodeLib.Symbologies;

/// <summary>
///  Telepen encoding
///  Written by: Brad Barnhill
/// </summary>
class Telepen : BarcodeCommon, IBarcode
{
    private static readonly Hashtable Telepen_Code = new Hashtable();
    private enum StartStopCode : int { START1, STOP1, START2, STOP2, START3, STOP3 };
    private StartStopCode _startCode = StartStopCode.START1;
    private StartStopCode _stopCode = StartStopCode.STOP1;
    private int _switchModeIndex;
    private int _iCheckSum;

    /// <summary>
    /// Encodes data using the Telepen algorithm.
    /// </summary>
    /// <param name="input"></param>
    public Telepen(string input)
    {
        RawData = input;
    }

    /// <summary>
    /// Encode the raw data using the Telepen algorithm.
    /// </summary>
    private string Encode_Telepen()
    {
        //only init if needed
        if (Telepen_Code.Count == 0)
            Init_Telepen();

        _iCheckSum = 0;
        var result = "";

        SetEncodingSequence();

        //include the Start sequence pattern
        result = Telepen_Code[_startCode].ToString();

        switch (_startCode)
        {
            //numeric --> ascii
            case StartStopCode.START2:
                EncodeNumeric(RawData.Substring(0, _switchModeIndex), ref result);

                if (_switchModeIndex < RawData.Length)
                {
                    EncodeSwitchMode(ref result);
                    EncodeASCII(RawData.Substring(_switchModeIndex), ref result);
                }//if
                break;
            //ascii --> numeric
            case StartStopCode.START3:
                EncodeASCII(RawData.Substring(0, _switchModeIndex), ref result);
                EncodeSwitchMode(ref result);
                EncodeNumeric(RawData.Substring(_switchModeIndex), ref result);
                break;
            //full ascii
            default:
                EncodeASCII(RawData, ref result);
                break;
        }//switch

        //checksum
        result += Telepen_Code[Calculate_Checksum(_iCheckSum)];

        //stop character
        result += Telepen_Code[_stopCode];

        return result;
    }//Encode_Telepen

    private void EncodeASCII(string input, ref string output)
    {
        try
        {
            foreach (var c in input)
            {
                output += Telepen_Code[c];
                _iCheckSum += Convert.ToInt32(c);
            }//foreach
        }//try
        catch
        {
            Error("ETELEPEN-1: Invalid data when encoding ASCII");
        }//catch
    }
    private void EncodeNumeric(string input, ref string output)
    {
        try
        {
            if ((input.Length % 2) > 0)
                Error("ETELEPEN-3: Numeric encoding attempted on odd number of characters");

            for (var i = 0; i < input.Length; i += 2)
            {
                output += Telepen_Code[Convert.ToChar(Int32.Parse(input.Substring(i, 2)) + 27)];
                _iCheckSum += Int32.Parse(input.Substring(i, 2)) + 27;
            }//for
        }//try
        catch
        {
            Error("ETELEPEN-2: Numeric encoding failed");
        }//catch
    }
    private void EncodeSwitchMode(ref string output)
    {
        //ASCII code DLE is used to switch modes
        _iCheckSum += 16;
        output += Telepen_Code[Convert.ToChar(16)];
    }

    private char Calculate_Checksum(int iCheckSum)
    {
        return Convert.ToChar(127 - (iCheckSum % 127));
    }//Calculate_Checksum(string)

    private void SetEncodingSequence()
    {
        //reset to full ascii
        _startCode = StartStopCode.START1;
        _stopCode = StartStopCode.STOP1;
        _switchModeIndex = RawData.Length;

        //starting number of 'numbers'
        var StartNumerics = 0;
        foreach (var c in RawData)
        {
            if (Char.IsNumber(c))
                StartNumerics++;
            else
                break;
        }//foreach

        if (StartNumerics == RawData.Length)
        {
            //Numeric only mode due to only numbers being present
            _startCode = StartStopCode.START2;
            _stopCode = StartStopCode.STOP2;

            if ((RawData.Length % 2) > 0)
                _switchModeIndex = RawData.Length - 1;
        }//if
        else
        {
            //ending number of numbers
            var EndNumerics = 0;
            for (var i = RawData.Length - 1; i >= 0; i--)
            {
                if (Char.IsNumber(RawData[i]))
                    EndNumerics++;
                else
                    break;
            }//for

            if (StartNumerics >= 4 || EndNumerics >= 4)
            {
                //hybrid mode will be used
                if (StartNumerics > EndNumerics)
                {
                    //start in numeric switching to ascii
                    _startCode = StartStopCode.START2;
                    _stopCode = StartStopCode.STOP2;
                    _switchModeIndex = (StartNumerics % 2) == 1 ? StartNumerics - 1 : StartNumerics;
                }//if
                else
                {
                    //start in ascii switching to numeric
                    _startCode = StartStopCode.START3;
                    _stopCode = StartStopCode.STOP3;
                    _switchModeIndex = (EndNumerics % 2) == 1 ? RawData.Length - EndNumerics + 1 : RawData.Length - EndNumerics;
                }//else
            }//if
        }//else
    }//SetEncodingSequence

    private void Init_Telepen()
    {
        Telepen_Code.Add(Convert.ToChar(0), "1110111011101110");
        Telepen_Code.Add(Convert.ToChar(1), "1011101110111010");
        Telepen_Code.Add(Convert.ToChar(2), "1110001110111010");
        Telepen_Code.Add(Convert.ToChar(3), "1010111011101110");
        Telepen_Code.Add(Convert.ToChar(4), "1110101110111010");
        Telepen_Code.Add(Convert.ToChar(5), "1011100011101110");
        Telepen_Code.Add(Convert.ToChar(6), "1000100011101110");
        Telepen_Code.Add(Convert.ToChar(7), "1010101110111010");
        Telepen_Code.Add(Convert.ToChar(8), "1110111000111010");
        Telepen_Code.Add(Convert.ToChar(9), "1011101011101110");
        Telepen_Code.Add(Convert.ToChar(10), "1110001011101110");
        Telepen_Code.Add(Convert.ToChar(11), "1010111000111010");
        Telepen_Code.Add(Convert.ToChar(12), "1110101011101110");
        Telepen_Code.Add(Convert.ToChar(13), "1010001000111010");
        Telepen_Code.Add(Convert.ToChar(14), "1000101000111010");
        Telepen_Code.Add(Convert.ToChar(15), "1010101011101110");
        Telepen_Code.Add(Convert.ToChar(16), "1110111010111010");
        Telepen_Code.Add(Convert.ToChar(17), "1011101110001110");
        Telepen_Code.Add(Convert.ToChar(18), "1110001110001110");
        Telepen_Code.Add(Convert.ToChar(19), "1010111010111010");
        Telepen_Code.Add(Convert.ToChar(20), "1110101110001110");
        Telepen_Code.Add(Convert.ToChar(21), "1011100010111010");
        Telepen_Code.Add(Convert.ToChar(22), "1000100010111010");
        Telepen_Code.Add(Convert.ToChar(23), "1010101110001110");
        Telepen_Code.Add(Convert.ToChar(24), "1110100010001110");
        Telepen_Code.Add(Convert.ToChar(25), "1011101010111010");
        Telepen_Code.Add(Convert.ToChar(26), "1110001010111010");
        Telepen_Code.Add(Convert.ToChar(27), "1010100010001110");
        Telepen_Code.Add(Convert.ToChar(28), "1110101010111010");
        Telepen_Code.Add(Convert.ToChar(29), "1010001010001110");
        Telepen_Code.Add(Convert.ToChar(30), "1000101010001110");
        Telepen_Code.Add(Convert.ToChar(31), "1010101010111010");
        Telepen_Code.Add(' ', "1110111011100010");
        Telepen_Code.Add('!', "1011101110101110");
        Telepen_Code.Add('"', "1110001110101110");
        Telepen_Code.Add('#', "1010111011100010");
        Telepen_Code.Add('$', "1110101110101110");
        Telepen_Code.Add('%', "1011100011100010");
        Telepen_Code.Add('&', "1000100011100010");
        Telepen_Code.Add('\'', "1010101110101110");
        Telepen_Code.Add('(', "1110111000101110");
        Telepen_Code.Add(')', "1011101011100010");
        Telepen_Code.Add('*', "1110001011100010");
        Telepen_Code.Add('+', "1010111000101110");
        Telepen_Code.Add(',', "1110101011100010");
        Telepen_Code.Add('-', "1010001000101110");
        Telepen_Code.Add('.', "1000101000101110");
        Telepen_Code.Add('/', "1010101011100010");
        Telepen_Code.Add('0', "1110111010101110");
        Telepen_Code.Add('1', "1011101000100010");
        Telepen_Code.Add('2', "1110001000100010");
        Telepen_Code.Add('3', "1010111010101110");
        Telepen_Code.Add('4', "1110101000100010");
        Telepen_Code.Add('5', "1011100010101110");
        Telepen_Code.Add('6', "1000100010101110");
        Telepen_Code.Add('7', "1010101000100010");
        Telepen_Code.Add('8', "1110100010100010");
        Telepen_Code.Add('9', "1011101010101110");
        Telepen_Code.Add(':', "1110001010101110");
        Telepen_Code.Add(';', "1010100010100010");
        Telepen_Code.Add('<', "1110101010101110");
        Telepen_Code.Add('=', "1010001010100010");
        Telepen_Code.Add('>', "1000101010100010");
        Telepen_Code.Add('?', "1010101010101110");
        Telepen_Code.Add('@', "1110111011101010");
        Telepen_Code.Add('A', "1011101110111000");
        Telepen_Code.Add('B', "1110001110111000");
        Telepen_Code.Add('C', "1010111011101010");
        Telepen_Code.Add('D', "1110101110111000");
        Telepen_Code.Add('E', "1011100011101010");
        Telepen_Code.Add('F', "1000100011101010");
        Telepen_Code.Add('G', "1010101110111000");
        Telepen_Code.Add('H', "1110111000111000");
        Telepen_Code.Add('I', "1011101011101010");
        Telepen_Code.Add('J', "1110001011101010");
        Telepen_Code.Add('K', "1010111000111000");
        Telepen_Code.Add('L', "1110101011101010");
        Telepen_Code.Add('M', "1010001000111000");
        Telepen_Code.Add('N', "1000101000111000");
        Telepen_Code.Add('O', "1010101011101010");
        Telepen_Code.Add('P', "1110111010111000");
        Telepen_Code.Add('Q', "1011101110001010");
        Telepen_Code.Add('R', "1110001110001010");
        Telepen_Code.Add('S', "1010111010111000");
        Telepen_Code.Add('T', "1110101110001010");
        Telepen_Code.Add('U', "1011100010111000");
        Telepen_Code.Add('V', "1000100010111000");
        Telepen_Code.Add('W', "1010101110001010");
        Telepen_Code.Add('X', "1110100010001010");
        Telepen_Code.Add('Y', "1011101010111000");
        Telepen_Code.Add('Z', "1110001010111000");
        Telepen_Code.Add('[', "1010100010001010");
        Telepen_Code.Add('\\', "1110101010111000");
        Telepen_Code.Add(']', "1010001010001010");
        Telepen_Code.Add('^', "1000101010001010");
        Telepen_Code.Add('_', "1010101010111000");
        Telepen_Code.Add('`', "1110111010001000");
        Telepen_Code.Add('a', "1011101110101010");
        Telepen_Code.Add('b', "1110001110101010");
        Telepen_Code.Add('c', "1010111010001000");
        Telepen_Code.Add('d', "1110101110101010");
        Telepen_Code.Add('e', "1011100010001000");
        Telepen_Code.Add('f', "1000100010001000");
        Telepen_Code.Add('g', "1010101110101010");
        Telepen_Code.Add('h', "1110111000101010");
        Telepen_Code.Add('i', "1011101010001000");
        Telepen_Code.Add('j', "1110001010001000");
        Telepen_Code.Add('k', "1010111000101010");
        Telepen_Code.Add('l', "1110101010001000");
        Telepen_Code.Add('m', "1010001000101010");
        Telepen_Code.Add('n', "1000101000101010");
        Telepen_Code.Add('o', "1010101010001000");
        Telepen_Code.Add('p', "1110111010101010");
        Telepen_Code.Add('q', "1011101000101000");
        Telepen_Code.Add('r', "1110001000101000");
        Telepen_Code.Add('s', "1010111010101010");
        Telepen_Code.Add('t', "1110101000101000");
        Telepen_Code.Add('u', "1011100010101010");
        Telepen_Code.Add('v', "1000100010101010");
        Telepen_Code.Add('w', "1010101000101000");
        Telepen_Code.Add('x', "1110100010101000");
        Telepen_Code.Add('y', "1011101010101010");
        Telepen_Code.Add('z', "1110001010101010");
        Telepen_Code.Add('{', "1010100010101000");
        Telepen_Code.Add('|', "1110101010101010");
        Telepen_Code.Add('}', "1010001010101000");
        Telepen_Code.Add('~', "1000101010101000");
        Telepen_Code.Add(Convert.ToChar(127), "1010101010101010");
        Telepen_Code.Add(StartStopCode.START1, "1010101010111000");
        Telepen_Code.Add(StartStopCode.STOP1, "1110001010101010");
        Telepen_Code.Add(StartStopCode.START2, "1010101011101000");
        Telepen_Code.Add(StartStopCode.STOP2, "1110100010101010");
        Telepen_Code.Add(StartStopCode.START3, "1010101110101000");
        Telepen_Code.Add(StartStopCode.STOP3, "1110101000101010");
    }

    #region IBarcode Members

    public string Encoded_Value => Encode_Telepen();

    #endregion
}
