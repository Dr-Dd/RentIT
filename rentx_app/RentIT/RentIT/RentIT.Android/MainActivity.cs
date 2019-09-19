using System;
using RentIT;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RentIT.Droid.Modules;
using CarouselView.FormsPlugin.Android;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Xamd.ImageCarousel.Forms.Plugin.Droid;
using DLToolkit.Forms.Controls;

namespace RentIT.Droid
{
    [Activity(Label = "RentIT", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Instance = this;
            

            base.OnCreate(savedInstanceState);
            InitControls();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new RentITPlatformModule()));
        }

        private void InitControls()
        {
            CarouselViewRenderer.Init();
            ImageCarouselRenderer.Init();
            FlowListView.Init();
        }

        //Lettura immagini dalla galleria
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}