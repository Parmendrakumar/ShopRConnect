using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using RetailerApp.Droid.CustomRenderer;
using RetailerApp.CustomControls;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.V4.Content;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace RetailerApp.Droid.CustomRenderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                Control.Background = AddPickerStyles(element.Image);

            }
        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Black;

            border.SetPadding(0, 0, 0, 0);


            border.Paint.SetStyle(Paint.Style.Stroke);

            Drawable[] layers = { border, GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);


            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;


            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 20, 20, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }

    }
}