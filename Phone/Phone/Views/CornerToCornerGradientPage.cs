﻿using System;

using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using Android.Content;

namespace Phone.Views
{
	public class CornerToCornerGradientPage : ContentPage
	{
        bool drawBackground;
        public CornerToCornerGradientPage (Context context)
		{
            Title = "Corner-to-Corner Gradient";            
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            BindingContext = canvasView;
            Content = canvasView;
		}
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            using (SKPaint paint = new SKPaint())
            {                
                // Create linear gradient from upper-left to lower-right
                paint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(rect.Left, rect.Top),
                                    new SKPoint(rect.Right, rect.Bottom),
                                    new SKColor[] { SKColors.Red, SKColors.Blue},
                                    new float[] { 0, 1 },
                                    SKShaderTileMode.Repeat);
                // Draw the gradient on the rectangle
                canvas.DrawRect(rect, paint);

                if (drawBackground)
                {
                    // Draw the gradient on the whole canvas
                    canvas.DrawRect(info.Rect, paint);

                    // Outline the smaller rectangle
                    paint.Shader = null;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = SKColors.Black;
                    canvas.DrawRect(rect, paint);
                }
            }
        }
    }
}