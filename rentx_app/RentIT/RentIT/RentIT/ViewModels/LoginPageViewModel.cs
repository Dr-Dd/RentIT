using RentIT.Models;
using RentIT.Models.User;
using RentIT.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using RentIT.Services.Authentication;
using System.Text;

namespace RentIT.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        readonly AuthenticationService _authService;
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

        //Comando di log in
        Command _logInCommand;
        public Command LogInCommand
        {
            get
            {
                return _logInCommand
                    ?? (_logInCommand = new Command(async () => await ExecuteLogInCommand()));
            }
        }

        async Task ExecuteLogInCommand()
        {
            IsBusy = true;

            Email = Email.Trim();
            Password = Password.Trim();

            var authResponse = await _authService.LoginAsync(Email, Password);
            if (authResponse.hasSucceded)
            {
                if (AppSettings.NewProfile)
                {
                    StringBuilder benvenuto = new StringBuilder();
                    benvenuto.AppendLine("Benvenuto su RentIT!");
                    benvenuto.Append("Completa il tuo profilo prima di continuare");
                    await App.Current.MainPage.DisplayAlert("RentIT", benvenuto.ToString(), "Ok");
                    await NavService.NavigateTo<ModificaDatiViewModel>();
                    return;
                }
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", authResponse.responseMessage, "Ok");
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

        //Comando di reset password
        Command _resetPasswordCommand;
        public Command ResetPasswordCommand
        {
            get
            {
                return _resetPasswordCommand
                    ?? (_resetPasswordCommand = new Command(async () => await ExecuteResetPasswordCommand()));
            }
        }

        async Task ExecuteResetPasswordCommand()
        {
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(Email)) {
                StringBuilder benvenuto = new StringBuilder();
                benvenuto.AppendLine("Inserisci la mail nel form qui sopra");
                await App.Current.MainPage.DisplayAlert("RentIT", benvenuto.ToString(), "Ok");
                return;
            }

                Email = Email.Trim();
            
            var authResponse = await _authService.resetPassword(Email);
            if (authResponse.hasSucceded)
            {
                StringBuilder benvenuto = new StringBuilder();
                benvenuto.AppendLine("Ti abbiamo inviato la password sostituitiva.");
                benvenuto.Append("Controlla la tua mail");
                await App.Current.MainPage.DisplayAlert("RentIT", benvenuto.ToString(), "Ok");
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", authResponse.responseMessage, "Ok");
            }

            IsBusy = false;
        }
    }
}
