using RentIT.Models;
using RentIT.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace RentIT.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(INavService navService) : base(navService)
        {
            // TODO: Ancora non è ben chiaro cosa andrebbe costruito, 
            // probabilmente servirebbe creare QUI il credentialsManager
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
            // TODO: non è ben chiaro cosa serva all'avvio dell'applicazione
        }

        /**
         * Comando di log in, NB: IL PARAMETRO UTENTE E' TEMPORANEO, un 
         * segnaposto insomma*/
        Command<Utente> _signInCommand;
        public Command<Utente> SignInCommand
        {
            get
            {
                return _signInCommand
                    ?? (_signInCommand = new Command<Utente>(async (utente) => await ExecuteSignInCommand(utente)));
            }
        }

        async Task ExecuteSignInCommand(Utente utente)
        {
            MessagingCenter.Send(this, "ATTENZIONE ATTENZIONE", "Il metodo non è ancora stato implementato.");
        }

        /**
         * Comando di registrazione (sign up), vedi sotto */
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
         * di sign up (registrazione), non di submit (trasmissione di info) */
        async Task ExecuteSubmitCommand()
        {
            await NavService.NavigateTo<SubmitPageViewModel>();
        }
    }
}