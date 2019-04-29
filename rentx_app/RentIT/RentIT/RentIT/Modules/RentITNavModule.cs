using Ninject.Modules;
using RentIT.Services;
using RentIT.ViewModels;
using RentIT.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RentIT.Modules
{
    public class RentITNavModule : NinjectModule
    {
        readonly INavigation _xfNav;

        public RentITNavModule(INavigation xamarinFormsNavigation)
        {
            _xfNav = xamarinFormsNavigation;
        }

        public override void Load()
        {
            var navService = new XamarinFormsNavService();
            navService.XamarinFormsNav = _xfNav;

            // Ricordarsi di aggiungere sempre alla mappa ogni ViewModel-View
            navService.RegisterViewMapping(typeof(LoginPageViewModel),
                typeof(LoginPage));

            navService.RegisterViewMapping(typeof(SubmitPageViewModel),
                typeof(SubmitPage));

            navService.RegisterViewMapping(typeof(SearchPageViewModel),
                typeof(SearchPage));


            Bind<INavService>()
                .ToMethod(x => navService)
                .InSingletonScope();
        }
    }
}
