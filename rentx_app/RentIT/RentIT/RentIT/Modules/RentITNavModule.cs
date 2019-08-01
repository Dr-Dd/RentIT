
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

            navService.RegisterViewMapping(typeof(SearchPageDetailViewModel),
                typeof(SearchPageDetail));

            navService.RegisterViewMapping(typeof(AnnunciPageViewModel),
                typeof(AnnunciPage));

            navService.RegisterViewMapping(typeof(UtentePageViewModel),
                typeof(UtentePage));

            navService.RegisterViewMapping(typeof(DettaglioAnnuncioViewModel),
                typeof(DettaglioAnnuncioPage));

            navService.RegisterViewMapping(typeof(ModificaDatiViewModel),
                typeof(ModificaDati));

            navService.RegisterViewMapping(typeof(AggiungiAnnuncioViewModel),
                typeof(AggiungiAnnuncioPage));

            navService.RegisterViewMapping(typeof(ModificaPasswordViewModel),
                typeof(ModificaPassword));

            navService.RegisterViewMapping(typeof(ModificaEmailViewModel),
                typeof(ModificaEmail));

            navService.RegisterViewMapping(typeof(AnnunciUtenteViewModel),
                typeof(AnnunciUtentePage));

            navService.RegisterViewMapping(typeof(GestioneAnnuncioViewModel),
                typeof(GestioneAnnuncioPage));

            Bind<INavService>()
                .ToMethod(x => navService)
                .InSingletonScope();
        }
    }
}
