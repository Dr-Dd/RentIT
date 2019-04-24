using RentIT.Data;
using RentIT.Services;
using RentIT.Views;
using RentIT.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RentIT
{
    public partial class App : Application
    {
        public static CredentialsManager CredManager { get; private set; }

        public App()
        {
            CredManager = new CredentialsManager(new RestService());

            var mainPage = new NavigationPage(new LoginPage());
            var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;

            navService.XamarinFormsNav = mainPage.Navigation;

            navService.RegisterViewMapping(typeof(LoginPageViewModel),
                typeof(LoginPage));

            navService.RegisterViewMapping(typeof(SubmitPageViewModel),
                typeof(SubmitPage));

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