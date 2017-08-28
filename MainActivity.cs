using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace CountingApp
{
    [Activity(Label = "CountingApp", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
        }
    }
}

