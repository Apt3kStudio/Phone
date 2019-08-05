using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Controls
{    
    public class SkiaCustomControl : SKCanvasView
    {       
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            //var canvas = e.Surface.Canvas;// Rounded rect
            //var radius = bounds.Height / 2;
            //canvas.DrawRoundRect(bounds, radius, radius, greenPaint);

            //// Circle
            //canvas.DrawCircle(bounds.Width / 4, bounds.MidY, radius, orangePaint);

            //// Rectangle  
            //var rect = new SKRect(bounds.Right - bounds.Height, bounds.Top, bounds.Right, bounds.Bottom);
            //canvas.DrawRect(rect, purplePaint);
        }
    }
}
