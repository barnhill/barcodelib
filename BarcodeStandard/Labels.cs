using SkiaSharp;
using System;
using System.Drawing;
using BarcodeStandard;

namespace BarcodeLib
{
    class Labels
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
                float fontHeight;
                using (var canvas = new SKCanvas(img))
                {
                    //color a box at the bottom of the barcode to hold the string of data
                    using(var paint = new SKPaint(font)) 
                    {
                        paint.FilterQuality = SKFilterQuality.High;
                        paint.IsAntialias = true;
                        paint.ColorF = barcode.BackColor;
                        paint.Style = SKPaintStyle.Fill;

                        fontHeight = paint.MeasureText("A");

                        var rect = SKRect.Create(0, img.Height - (fontHeight - 2), img.Width, fontHeight);
                        canvas.DrawRect(rect, paint);
                    }
                    
                    //draw datastring under the barcode image
                    using (var foreBrush = new SKPaint(font))
                    {
                        foreBrush.FilterQuality = SKFilterQuality.High;
                        foreBrush.IsAntialias = true;
                        foreBrush.ColorF = barcode.ForeColor;
                        foreBrush.TextAlign = SKTextAlign.Center;

                        var str = barcode.AlternateLabel ?? barcode.RawData;
                        var point = new SKPoint(img.Width / 2f, img.Height - fontHeight + 1);
                        canvas.DrawText(str, point, foreBrush);
                    }

                    using (var pen = new SKPaint())
                    {
                        pen.FilterQuality = SKFilterQuality.High;
                        pen.IsAntialias = true;
                        pen.ColorF = barcode.ForeColor;
                        pen.StrokeWidth = (float)img.Height / 16;

                        canvas.DrawLine(new SKPoint(0, img.Height - (int)fontHeight - 2), new SKPoint(img.Width, img.Height - (int)fontHeight - 2), pen);//bottom
                    }

                    canvas.Save();
                }//using

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
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        /*public static SKImage Label_Generic(Barcode Barcode, SKBitmap img)
        {
            try
            {
                SKFont font = Barcode.LabelFont;

                using (SKCanvas g = new SKCanvas(img))
                {
                    g.DrawImage(SKImage.FromBitmap(img), 0f, 0f);

                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    StringFormat f = new StringFormat();
                    f.Alignment = StringAlignment.Near;
                    f.LineAlignment = StringAlignment.Near;
                    int LabelX = 0;
                    int LabelY = 0;

                    switch (Barcode.LabelPosition)
                    {
                        case LabelPositions.BOTTOMCENTER:
                            LabelX = img.Width / 2;
                            LabelY = img.Height - (font.Height);
                            f.Alignment = StringAlignment.Center;
                            break;
                        case LabelPositions.BOTTOMLEFT:
                            LabelX = 0;
                            LabelY = img.Height - (font.Height);
                            f.Alignment = StringAlignment.Near;
                            break;
                        case LabelPositions.BOTTOMRIGHT:
                            LabelX = img.Width;
                            LabelY = img.Height - (font.Height);
                            f.Alignment = StringAlignment.Far;
                            break;
                        case LabelPositions.TOPCENTER:
                            LabelX = img.Width / 2;
                            LabelY = 0;
                            f.Alignment = StringAlignment.Center;
                            break;
                        case LabelPositions.TOPLEFT:
                            LabelX = img.Width;
                            LabelY = 0;
                            f.Alignment = StringAlignment.Near;
                            break;
                        case LabelPositions.TOPRIGHT:
                            LabelX = img.Width;
                            LabelY = 0;
                            f.Alignment = StringAlignment.Far;
                            break;
                    }//switch

                    //color a background color box at the bottom of the barcode to hold the string of data
                    using (SolidBrush backBrush = new SolidBrush(Barcode.BackColor))
                    {
                        g.FillRectangle(backBrush, new RectangleF((float)0, (float)LabelY, (float)img.Width, (float)font.Height));
                    }

                    //draw datastring under the barcode image
                    using (SolidBrush foreBrush = new SolidBrush(Barcode.ForeColor))
                    {
                        g.DrawString(Barcode.AlternateLabel == null ? Barcode.RawData : Barcode.AlternateLabel, font, foreBrush, new RectangleF((float)0, (float)LabelY, (float)img.Width, (float)font.Height), f);
                    }

                    g.Save();
                }//using
                return img;
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
            }//catch
        }//Label_Generic*/


        /// <summary>
        /// Draws Label for EAN-13 barcodes
        /// </summary>
        /// <param name="barcode">Barcode to draw label for.</param>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static SKImage Label_EAN13(Barcode barcode, SKBitmap img)
        {
            if (barcode == null) throw new ArgumentNullException(nameof(barcode));
            try
            {
                var iBarWidth = barcode.Width / barcode.EncodedValue.Length;
                var defTxt = barcode.RawData;

                using (var labFont = new SKFont(SKTypeface.Default, GetFontSize(barcode, barcode.Width - barcode.Width % barcode.EncodedValue.Length, img.Height, defTxt)))
                {
                    int shiftAdjustment = GetAlignmentShiftAdjustment(barcode);

                    using (var g = new SKCanvas(img))
                    {
                        float labFontHeight;
                        using (var fontBrush = new SKPaint(labFont))
                        {
                            labFontHeight = fontBrush.MeasureText("A");
                        }
                        
                        int labelY;

                        //Default alignment for EAN13
                        labelY = img.Height - (int)labFontHeight;

                        float w1 = iBarWidth * 4; //Width of first block
                        float w2 = iBarWidth * 42; //Width of second block
                        float w3 = iBarWidth * 42; //Width of third block

                        float s1 = shiftAdjustment - iBarWidth;
                        var s2 = s1 + (iBarWidth * 4); //Start position of block 2
                        var s3 = s2 + w2 + (iBarWidth * 5); //Start position of block 3

                        //Draw the background rectangles for each block
                        using (var backBrush = new SKPaint(labFont))
                        {
                            backBrush.FilterQuality = SKFilterQuality.High;
                            backBrush.ColorF = barcode.BackColor;
                            g.DrawRect(new SKRect(s2, labelY, w2, labFontHeight), backBrush);

                        }

                        //draw datastring under the barcode image
                        using (var foreBrush = new SKPaint())
                        {
                            foreBrush.FilterQuality = SKFilterQuality.High;
                            foreBrush.ColorF = barcode.ForeColor;
                            foreBrush.TextAlign = SKTextAlign.Left;
                            
                            using (var smallFont = new SKFont(labFont.Typeface, labFont.Size * 0.5f))
                            {
                                using (var foreBrushSmall = new SKPaint(smallFont))
                                {
                                    foreBrushSmall.FilterQuality = SKFilterQuality.High;
                                    foreBrushSmall.ColorF = barcode.ForeColor;
                                    foreBrushSmall.TextAlign = SKTextAlign.Left;
                                    g.DrawText(defTxt.Substring(0, 1), s1, img.Width, foreBrushSmall);
                                }
                            }
                            
                            g.DrawText(defTxt.Substring(1, 6), s2, labelY, foreBrush);
                            g.DrawText(defTxt.Substring(7), s3 - iBarWidth, labelY, foreBrush);
                        }

                        g.Save();
                    }
                }//using

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
                var halfBarWidth = (int)(iBarWidth * 0.5);
                var defTxt = barcode.RawData;
                
                using (var labFont = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), GetFontSize(barcode, (int)((barcode.Width - barcode.Width % barcode.EncodedValue.Length) * 0.9f), img.Height, defTxt)))
                {
                    int shiftAdjustment = GetAlignmentShiftAdjustment(barcode);

                    using (var surface = SKSurface.Create(new SKImageInfo().WithSize(img.Width, img.Height)))
                    {
                        surface.Canvas.DrawImage(SKImage.FromBitmap(img), new SKPoint(0, 0));
                        var g = surface.Canvas;
                        g.DrawImage(SKImage.FromBitmap(img), 0, 0);

                        labFont.GetFontMetrics(out var fontMetrics);

                        //Default alignment for UPCA
                        var labelY = (int)(img.Height - fontMetrics.XHeight);

                        /*f.Alignment = StringAlignment.Near;*/
                        float w1 = iBarWidth * 4; //Width of first block
                        float w2 = iBarWidth * 34; //Width of second block
                        float w3 = iBarWidth * 34; //Width of third block

                        float s1 = shiftAdjustment - iBarWidth;
                        var s2 = s1 + (iBarWidth * 12); //Start position of block 2
                        var s3 = s2 + w2 + (iBarWidth * 5); //Start position of block 3
                        var s4 = s3 + w3 + (iBarWidth * 8) - halfBarWidth;
                        
                        //Draw the background rectangles for each block
                        using (var backBrush = new SKPaint())
                        {
                            backBrush.ColorF = barcode.BackColor;

                            g.DrawRect(new SKRect(s2, labelY, w2, fontMetrics.XHeight), backBrush);
                            g.DrawRect(new SKRect(s3, labelY, w3, fontMetrics.XHeight), backBrush);
                        }

                        using (var labFontSmall = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), GetFontSize(barcode, (int)((barcode.Width - barcode.Width % barcode.EncodedValue.Length) * 0.9f * 0.5), img.Height, defTxt)))
                        {
                            //draw data string under the barcode image
                            using (var foreBrushSmall = new SKPaint(labFontSmall))
                            {
                                using (var foreBrushLarge = new SKPaint(labFont))
                                {
                                    foreBrushLarge.ColorF = barcode.ForeColor;
                                    foreBrushSmall.ColorF = barcode.ForeColor;

                                    labFontSmall.GetFontMetrics(out fontMetrics);

                                    g.DrawText(defTxt.Substring(0, 1), s1, img.Height - fontMetrics.XHeight, foreBrushSmall);
                                    g.DrawText(defTxt.Substring(1, 5), s2 - iBarWidth, labelY, foreBrushLarge);
                                    g.DrawText(defTxt.Substring(6, 5), s3 - iBarWidth, labelY, foreBrushLarge);
                                    g.DrawText(defTxt.Substring(11), s4, img.Height - fontMetrics.XHeight, foreBrushSmall);
                                }
                            }
                        }

                        g.Save();
                        
                        return surface.Snapshot();
                    } 
                }//using
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_UPCA-1: " + ex.Message);
            }//catch
        }//Label_UPCA

        private static int GetAlignmentShiftAdjustment(Barcode barcode)
        {
            switch (barcode.Alignment)
            {
                case AlignmentPositions.Left:
                    return 0;
                case AlignmentPositions.Right:
                    return (barcode.Width % barcode.EncodedValue.Length);
                case AlignmentPositions.Center:
                default:
                    return (barcode.Width % barcode.EncodedValue.Length) / 2;
            }//switch
        }

        public static int GetFontSize(Barcode barcode, int wid, int hgt, string lbl)
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
