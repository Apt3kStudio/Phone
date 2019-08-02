using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Utility.CustomControls
{
    public class CostomControl : SKCanvasView
    {
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {



            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            
            using (SKPaint tpaint = new SKPaint())
            {
                SKColor SpideyBlueBckGrnd;
                //SKColor.TryParse("#020f1f", out SpideyBlueBckGrnd);
                SKColor.TryParse("#ECE9E6", out SpideyBlueBckGrnd);
                SKColor SpideyLightBlueBckGrnd;
                SKColor.TryParse("#FFFFFF", out SpideyLightBlueBckGrnd);
                //SKColor.TryParse("#001c41", out SpideyLightBlueBckGrnd);
                tpaint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(info.Rect.Top, info.Rect.Top),
                                    new SKPoint(info.Rect.Bottom, info.Rect.Bottom),
                                    new SKColor[] { SpideyBlueBckGrnd, SpideyLightBlueBckGrnd },
                                    new float[] { 0, 10.80f },
                                    SKShaderTileMode.Mirror);
                canvas.DrawRect(info.Rect, tpaint);
            }


          
           
        }
    }
}
