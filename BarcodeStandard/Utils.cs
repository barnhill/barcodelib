using SkiaSharp;
using System;

namespace BarcodeStandard
{
    internal static class Utils
    {
        internal static int GetFontHeight(String text, SKFont font)
        {
            var textBounds = new SKRect();
            using (var textPaint = new SKPaint(font))
            {
                textPaint.MeasureText(text, ref textBounds);
                return (int)textBounds.Height;
            }
        }
    }
}
