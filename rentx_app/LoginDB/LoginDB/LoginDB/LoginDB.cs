using LoginDB.Data;
using LoginDB.Models;
using LoginDB.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LoginDB
{
    public class App : Application
    {
        public static CredentialsManager CredManager { get; private set; }


        public App()
        {
            CredManager = new CredentialsManager(new RestService());
            MainPage = new NavigationPage(new LoginPage());
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

