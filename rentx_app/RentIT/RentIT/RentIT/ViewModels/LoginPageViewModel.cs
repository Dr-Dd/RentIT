using RentIT.Models;
using RentIT.Models.User;
using RentIT.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using RentIT.Services.Authentication;

namespace RentIT.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        readonly IAuthenticationService _authService;
        public LoginPageViewModel(INavService navService, AuthenticationService authService) : base(navService)
        {
            // TODO: Ancora non è ben chiaro cosa andrebbe costruito, 
            // probabilmente servirebbe creare QUI il credentialsManager
            _authService = authService;
        }

        string _email;
        public string Email
        {
            get { return _email;  }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public override async Task Init()
        {
            // TODO: non è ben chiaro cosa serva all'avvio della pagina (forse nulla)
        }

        /**
         * Comando di log in, NB: IL PARAMETRO UTENTE E' TEMPORANEO, un 
         * segnaposto insomma
         */
        Command<Utente> _logInCommand;
        public Command<Utente> LogInCommand
        {
            get
            {
                return _logInCommand
                    ?? (_logInCommand = new Command<Utente>(async (utente) => await ExecuteLogInCommand(utente)));
            }
        }

        async Task ExecuteLogInCommand(Utente utente)
        {
            IsBusy = true;

            var authResponse = await _authService.LoginAsync(_email, _password);
            if (authResponse.HasSucceded)
            {
                await NavService.NavigateTo<SearchPageViewModel>();
                // await NavService.GoBack();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", authResponse.ResponseMessage, "Ok");
            }

            IsBusy = false;
        }

        /**
         * Comando di registrazione (sign up), vedi sotto 
         */
        Command _submitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _submitCommand
                    ?? (_submitCommand = new Command(async () => await ExecuteSubmitCommand()));
            }
        }

        /**
         * Task di avvio del processo di registrazione,
         * non amo molto il nome della funzione, ma lo accetto
         * lo stesso visto che in futuro cambierà (e' una procedura 
         * di sign up (registrazione), non di submit (trasmissione di info) 
         */
        async Task ExecuteSubmitCommand()
        {
            await NavService.NavigateTo<SubmitPageViewModel>();
        }
    }
}
