using Ninject.Modules;
using RentIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.Modules
{
    public class RentITCoreModule : NinjectModule
    {
        public override void Load()
        {
            // Carico i ViewModels
            Bind<LoginPageViewModel>().ToSelf();
            Bind<SearchPageViewModel>().ToSelf();
            Bind<SearchPageDetailViewModel>().ToSelf();
            Bind<SubmitPageViewModel>().ToSelf();
            Bind<TilePageViewModel>().ToSelf();
        }
    }
}
