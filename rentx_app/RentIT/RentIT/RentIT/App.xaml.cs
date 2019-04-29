using RentIT.Data;
using RentIT.Services;
using RentIT.Views;
using RentIT.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ninject.Modules;
using Ninject;
using RentIT.Modules;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RentIT
{
    public partial class App : Application
    {
        public static CredentialsManager CredManager { get; private set; }

        public IKernel Kernel { get; set; }

        public App(params INinjectModule[] platformModules)
        {
            CredManager = new CredentialsManager(new RestService());

            //Cambiare l'istanza oggetto per modificare la pagina iniziale e ricordarsi di aggiornare il get del kernel
            var mainPage = new NavigationPage(new SearchPage());

            // Inizializiamo il kernel
            Kernel = new StandardKernel(
                new RentITCoreModule(),
                new RentITNavModule(mainPage.Navigation));

            Kernel.Load(platformModules);

            mainPage.BindingContext = Kernel.Get<SearchPageViewModel>();

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}