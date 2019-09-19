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
using Ninject.Modules;
using RentIT.Services.Immagini;

namespace RentIT.Droid.Modules
{
    public class RentITPlatformModule : NinjectModule
    {
        public override void Load()
        {
            var multiMediaPickerService = new MultiMediaPickerService();
            // TODO: Ancora non ha nulla da implementare
            Bind<IMultiMediaPickerService>()
                .ToMethod(x => multiMediaPickerService)
                .InSingletonScope();
        }
    }
}