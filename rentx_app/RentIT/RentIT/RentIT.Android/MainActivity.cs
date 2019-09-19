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
using Plugin.Permissions;

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
            LoadApplication(new App(MultiMediaPickerService.SharedInstance));
        }

        private void InitControls()
        {
            CarouselViewRenderer.Init();
            ImageCarouselRenderer.Init();
        }

        //Lettura immagini dalla galleria
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            MultiMediaPickerService.SharedInstance.OnActivityResult(requestCode, resultCode, data);
        }
}