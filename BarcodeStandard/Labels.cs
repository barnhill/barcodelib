using SkiaSharp;
using System;
using System.Drawing;
using BarcodeStandard;

namespace BarcodeLib
{
    internal class Labels
    {
        /// <summary>
        /// Draws Label for ITF-14 barcodes
        /// </summary>
        /// <param name="barcode">Barcode to draw label for.</param>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static SKImage Label_ITF14(Barcode barcode, SKBitmap img)
        {
            if (barcode == null) throw new ArgumentNullException(nameof(barcode));
            try
            {
                var font = barcode.LabelFont;
                var str = barcode.AlternateLabel ?? barcode.RawData;
                using (var foreBrush = new SKPaint(font))
                {
                    SKRect textBounds = new();
                    foreBrush.MeasureText(str, ref textBounds);
                    var labelPadding = textBounds.Height / 2f;
                    var backY = img.Height - textBounds.Height - labelPadding * 2f;

                    using (var canvas = new SKCanvas(img))
                    {
                        //draw bounding box side overdrawn by label
                        using (var pen = new SKPaint())
                        {
                            pen.FilterQuality = SKFilterQuality.High;
                            pen.IsAntialias = true;
                            pen.ColorF = barcode.ForeColor;
                            pen.StrokeWidth = (float)img.Height / 16;

                            canvas.DrawLine(new SKPoint(0, backY - pen.StrokeWidth / 2f),
                                new SKPoint(img.Width, backY - pen.StrokeWidth / 2f), pen); //bottom
                        }

                        //color a box at the bottom of the barcode to hold the string of data
                        using (var paint = new SKPaint(font))
                        {
                            paint.FilterQuality = SKFilterQuality.High;
                            paint.IsAntialias = true;
                            paint.ColorF = barcode.BackColor;
                            paint.Style = SKPaintStyle.Fill;

                            var rect = SKRect.Create(0, backY, img.Width, textBounds.Height + labelPadding * 2f);
                            canvas.DrawRect(rect, paint);
                        }

                        //draw datastring under the barcode image
                        foreBrush.FilterQuality = SKFilterQuality.High;
                        foreBrush.IsAntialias = true;
                        foreBrush.ColorF = barcode.ForeColor;
                        foreBrush.TextAlign = SKTextAlign.Center;

                        var labelX = img.Width / 2f;
                        var labelY = img.Height - textBounds.Height + labelPadding;

                        canvas.DrawText(str, labelX, labelY, foreBrush);

                        canvas.Save();
                    } //using
                }

                return SKImage.FromBitmap(img);
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_ITF14-1: " + ex.Message);
            }//catch
        }

        /// <summary>
        /// Draws Label for Generic barcodes
        /// </summary>
        /// <param name="barcode">Barcode to draw label for</param>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static SKImage Label_Generic(Barcode barcode, SKBitmap img, int width)
        {
            try
            {
                using var g = new SKCanvas(img);

                /*g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;*/

                var alignmentAdjustment = BarcodeCommon.GetAlignmentShiftAdjustment(barcode);
                var text = barcode.AlternateLabel ?? barcode.RawData;

                //draw datastring under the barcode image
                using var foreBrush = new SKPaint(barcode.LabelFont)
                {
                    ColorF = barcode.ForeColor,
                };
                foreBrush.IsAntialias = true;
                foreBrush.IsLinearText = true;
                foreBrush.IsDither = true;
                foreBrush.IsAutohinted = true;
                foreBrush.FilterQuality = SKFilterQuality.High;

                SKRect textBounds = new();
                foreBrush.MeasureText(text, ref textBounds);
                var labelPadding = textBounds.Height / 2f;

                var labelX = alignmentAdjustment + ((width - textBounds.Width) / 2f);
                var labelY = img.Height - textBounds.Height + labelPadding;
                var backY = img.Height - textBounds.Height - labelPadding * 2f;

                //color a background color box at the bottom of the barcode to hold the string of data
                using var backBrush = new SKPaint
                {
                    ColorF = barcode.BackColor,
                    Style = SKPaintStyle.Fill
                };

                g.DrawRect(SKRect.Create(0, backY, img.Width, textBounds.Height + labelPadding * 2f), backBrush);
                g.DrawText(text, labelX, labelY, foreBrush);

                g.Save();
                g.Dispose();
                foreBrush.Dispose();
                backBrush.Dispose();
                return SKImage.FromBitmap(img);
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
            }//catch
        }

        /// <summary>
        /// Draws Label for EAN-13 barcodes
        /// </summary>
        /// <param name="barcode">Barcode to draw label for.</param>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static SKImage Label_EAN13(Barcode barcode, SKBitmap img, int iBarWidth)
        {
            if (barcode == null) throw new ArgumentNullException(nameof(barcode));
            try
            {
                var shiftAdjustment = BarcodeCommon.GetAlignmentShiftAdjustment(barcode);
                var quietZoneAdjustment = BarcodeCommon.GetQuietZoneAdjustment(barcode);

                barcode.LabelFont.Edging = SKFontEdging.SubpixelAntialias;

                var text = barcode.RawData;
                var first = text.Substring(0, 1);
                var second = text.Substring(1, 6);
                var third = text.Substring(7, 6);

                using var g = new SKCanvas(img);

                using var foreBrush = new SKPaint(barcode.LabelFont);
                SKRect textBounds = new();
                foreBrush.MeasureText(text, ref textBounds);

                //Default alignment for UPCA

                float w1 = iBarWidth * 3; //Width of first block
                float w2 = iBarWidth * 42; //Width of second block
                float w3 = iBarWidth * 42; //Width of third block

                float s1 = shiftAdjustment;
                var s2 = s1 + w1; //Start position of block 2
                var s3 = s2 + w2 + iBarWidth * 5; //Start position of block 3
                var s4 = s3 + w3;

                SKRect textBounds1 = new();
                SKRect textBounds2 = new();
                SKRect textBounds3 = new();
                SKRect textBounds4 = new();

                foreBrush.MeasureText(first, ref textBounds1);
                foreBrush.MeasureText(second, ref textBounds2);
                foreBrush.MeasureText(third, ref textBounds3);

                //Draw the background rectangles for each block
                using (var backBrush = new SKPaint())
                {
                    backBrush.ColorF = barcode.BackColor;
                    backBrush.IsAntialias = true;

                    if (barcode.GuardBarsMode == GuardBarsMode.Enabled)
                    {
                        g.DrawRect(new SKRect(s1, img.Height - textBounds1.Height - textBounds1.Height / 4f, s1 + w1, img.Height), backBrush); // first guard bar cover
                    }
                    g.DrawRect(new SKRect(s3, img.Height - textBounds3.Height * 2f, s3 + w3, img.Height), backBrush); // middle bar cover

                    g.DrawRect(new SKRect(s2 + w2, img.Height - textBounds4.Height - textBounds4.Height / 4f, s3, img.Height), backBrush);
                    g.DrawRect(new SKRect(s2, img.Height - textBounds2.Height * 2f, s2 + w2, img.Height), backBrush);
                }

                foreBrush.ColorF = barcode.ForeColor;
                foreBrush.IsAntialias = true;
                foreBrush.IsDither = true;
                foreBrush.IsLinearText = true;
                foreBrush.IsAutohinted = true;
                foreBrush.FilterQuality = SKFilterQuality.High;

                if (barcode.GuardBarsMode == GuardBarsMode.Enabled)
                {
                    g.DrawText(first, s1 + (w1 / 2f - textBounds1.Width / 2f), img.Height, foreBrush);
                }
                else if (barcode.GuardBarsMode == GuardBarsMode.EnabledFirstCharOnQuietZone)
                {
                    g.DrawText(first, s1 - quietZoneAdjustment, img.Height + textBounds2.MidY, foreBrush);
                }

                g.DrawText(second, s2 + (w2 / 2f - textBounds2.Width / 2f), img.Height + textBounds2.MidY, foreBrush);
                g.DrawText(third, s3 + (w3 / 2f - textBounds3.Width / 2f), img.Height + textBounds3.MidY, foreBrush);

                g.Save();
                g.Dispose();
                foreBrush.Dispose();
                return SKImage.FromBitmap(img);
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_EAN13-1: " + ex.Message);
            }//catch
        }//Label_EAN13

        /// <summary>
        /// Draws Label for UPC-A barcodes
        /// </summary>
        /// <param name="barcode">Barcode to draw the label for</param>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static SKImage Label_UPCA(Barcode barcode, SKBitmap img)
        {
            if (barcode == null) throw new ArgumentNullException(nameof(barcode));
            try
            {
                var iBarWidth = barcode.Width / barcode.EncodedValue.Length;

                var shiftAdjustment = BarcodeCommon.GetAlignmentShiftAdjustment(barcode);

                barcode.LabelFont.Edging = SKFontEdging.SubpixelAntialias;

                var text = barcode.RawData;
                var first = text.Substring(0, 1);
                var second = text.Substring(1, 5);
                var third = text.Substring(6, 5);
                var fourth = text.Substring(11);

                using var g = new SKCanvas(img);

                using var foreBrush = new SKPaint(barcode.LabelFont);
                SKRect textBounds = new();
                foreBrush.MeasureText(text, ref textBounds);

                //Default alignment for UPCA

                float w1 = iBarWidth * 3; //Width of first block
                float w2 = iBarWidth * 42; //Width of second block
                float w3 = iBarWidth * 42; //Width of third block
                float w4 = iBarWidth * 3; //Width of fourth block

                float s1 = shiftAdjustment;
                var s2 = s1 + w1; //Start position of block 2
                var s3 = s2 + w2 + iBarWidth * 5; //Start position of block 3
                var s4 = s3 + w3;

                SKRect textBounds1 = new();
                SKRect textBounds2 = new();
                SKRect textBounds3 = new();
                SKRect textBounds4 = new();

                foreBrush.MeasureText(first, ref textBounds1);
                foreBrush.MeasureText(second, ref textBounds2);
                foreBrush.MeasureText(third, ref textBounds3);
                foreBrush.MeasureText(fourth, ref textBounds4);

                //Draw the background rectangles for each block
                using (var backBrush = new SKPaint())
                {
                    backBrush.ColorF = barcode.BackColor;
                    backBrush.IsAntialias = true;

                    g.DrawRect(new SKRect(s1, img.Height - textBounds1.Height - textBounds1.Height / 4f, s1 + w1, img.Height), backBrush); // first guard bar cover
                    g.DrawRect(new SKRect(s4, img.Height - textBounds4.Height - textBounds4.Height / 4f, s4 + w4, img.Height), backBrush); // end guard bar cover
                    g.DrawRect(new SKRect(s3, img.Height - textBounds3.Height * 2f, s3 + w3, img.Height), backBrush); // middle bar cover

                    g.DrawRect(new SKRect(s2 + w2, img.Height - textBounds4.Height - textBounds4.Height / 4f, s3, img.Height), backBrush);
                    g.DrawRect(new SKRect(s2, img.Height - textBounds2.Height * 2f, s2 + w2, img.Height), backBrush);
                }

                foreBrush.ColorF = barcode.ForeColor;
                foreBrush.IsAntialias = true;
                foreBrush.IsDither = true;
                foreBrush.IsLinearText = true;
                foreBrush.IsAutohinted = true;
                foreBrush.FilterQuality = SKFilterQuality.High;

                g.DrawText(first, s1 + (w1 / 2f - textBounds1.Width / 2f), img.Height, foreBrush);
                g.DrawText(second, s2 + (w2 / 2f - textBounds2.Width / 2f), img.Height + textBounds2.MidY, foreBrush);
                g.DrawText(third, s3 + (w3 / 2f - textBounds3.Width / 2f), img.Height + textBounds3.MidY, foreBrush);
                g.DrawText(fourth, s4 + (w4 / 2f - textBounds4.Width / 2f), img.Height, foreBrush);

                g.Save();
                g.Dispose();
                foreBrush.Dispose();
                return SKImage.FromBitmap(img);
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_UPCA-1: " + ex.Message);
            }//catch
        }//Label_UPCA

        private static int GetFontSize(Barcode barcode, int wid, int hgt, string lbl)
        {
            //Returns the optimal font size for the specified dimensions
            var fontSize = 10;

            if (lbl.Length > 0)
            {
                var bounds = SKRect.Empty;
                for (var i = 1; i <= 100; i++)
                {
                    using (var testFont = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), i))
                    {
                        // Make a Graphics object to measure the text.
                        using (var gr = new SKPaint(testFont))
                        {
                            gr.MeasureText(lbl, ref bounds);

                            if (!(bounds.Width > wid) && !(bounds.Height > hgt)) continue;
                            fontSize = i - 1;
                            break;
                        }
                    }
                }
            };

            return fontSize;
        }
    }
}