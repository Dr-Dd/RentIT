using RentIT.Models;
using RentIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();
        }

        async void OnSignInProcedure(object sender, EventArgs e)
        {
            // Crea una nuova variabile "Credentials"
            var cred = new Credentials()
            {
                // Setta nella variabile cred il testo di psw e mail
                // *PROVENIENTI DA UN NAMESPACE RESO VISIBILE SU XAML*
                // ===*** DA SETTARE VIA PROPERTIES ***==
                password = psw.Text,
                email = mail.Text
            };
            // Attende l'output di validazione delle credenziali
            var content = await App.CredManager.AreCredentialsValid(cred);
            // Printa quindi l'output di risposta (e la rende visibile)
            // ===*** DA SETTARE VIA PROPERTIES ***===
            risposta.Text = content;
            risposta.IsVisible = true; 
        }

        async void OnSubmitProcedure(object sender, EventArgs e)
        {
            // Crea un nuovo utente
            var user = new Utente();
            // Crea una nuova "SubmitPage"
            var submitPage = new SubmitPage();
            // imposta il binding context della submitPage a user 
            // (dovrebbe essere il viewModel)
            submitPage.BindingContext = user;
            // Attende che la submit page 
            await Navigation.PushAsync(submitPage);
        } 
    } 
 }
    
