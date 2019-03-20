using LoginDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginDB.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnSignInProcedure(object sender, EventArgs e)
        {
            var cred = new Credentials()
            {
                password = psw.Text,
                email = mail.Text
            };
            var content = await App.CredManager.AreCredentialsValid(cred);
            risposta.Text = content;
            risposta.IsVisible = true;


        }

        async void OnSubmitProcedure(object sender, EventArgs e)
        {
            var user = new Utente();
            var submitPage = new SubmitPage();
            submitPage.BindingContext = user;
            await Navigation.PushAsync(submitPage);
        }

        

    }

       
 }
    
