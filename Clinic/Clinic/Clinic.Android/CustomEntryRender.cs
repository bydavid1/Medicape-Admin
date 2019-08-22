using Android.Content;
using Clinic.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Clinic;
using Android.Views;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.V4.Content;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRender))]


namespace Clinic.Droid
{
    public class CustomEntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Android.App.Application.Context.GetDrawable(Resource.Drawable.RoundCornerbutton);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(10, 0, 0, 0);
            }
        }
    }

}
