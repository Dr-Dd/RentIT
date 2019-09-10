﻿using System.Threading.Tasks;
using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.User;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class SubmitPageViewModel : BaseViewModel
    {
        // Non c'è bisogno di creare il navService, visto che è creato dal BaseViewModel
        readonly IUserService _userService;
        public SubmitPageViewModel(INavService navService, UserService userService) : base(navService)
        {
            _userService = userService;
        }

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        string _email;
        public string Email
        {
            get { return _email; }
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
                SignInCommand.ChangeCanExecute();
            }
        }                                                 

        string _confermaPassword;
        public string ConfermaPassword
        {
            get { return _confermaPassword; }
            set
            {
                _confermaPassword = value;
                OnPropertyChanged();
                //SignInCommand.ChangeCanExecute(); // Nota come, nel viewModel, la funzione di set
            }                                     // viene eseguita ogni qualvolta l'utente modifica il campo
        }                                         // interessato. Questo vuol dire che possiamo usarla per eseguire
                                                  // delle operazioni ogniqualvolta un utente modifica il campo! 
                                                  // Qui, ad esempio, sto utilizzando una funzione dei comandi, che permette di 
                                                  // attivare o disattivare un elemento ui in base alla funzione booleana associata
                                                  // al comando interessato (vedi sotto)

        public override async Task Init()
        {
        }

        /**
         * Funzione di controllo password, viene attivata ogniqualvolta una delle due password viene modificata
         */
        bool PasswordsAreTheSameAndNotEmpty()
        {
            var isValid = (Password == ConfermaPassword && 
                !string.IsNullOrWhiteSpace(Password));

            return isValid;
        }

        //Funzione di controllo campi vuoti
        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(Name) ||
                        string.IsNullOrWhiteSpace(Surname) ||
                        string.IsNullOrWhiteSpace(Email) ||
                        string.IsNullOrWhiteSpace(Password);
            return empty;
        }

        /*comando di registrazione*/
        Command _signInCommand;
        public Command SignInCommand
        {
            get
            {
                return _signInCommand
                    ?? (_signInCommand = new Command(async () => await ExecuteSignInCommand()));
            }
        }
        
        async Task ExecuteSignInCommand()
        {
            IsBusy = true;

            if (!PasswordsAreTheSameAndNotEmpty())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Le due password non corrispondono", "Ok");
                return;
            }

            if (EmptyFields())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Non hai riempito uno o più campi", "Ok");
                return;
            }
            var signUpRequest = new SignUpRequest
            {
                name = Name,
                surname = Surname,
                email = Email,
                password = Password
            };
            var signUpResponse = await _userService.SignUpAsync(signUpRequest);
            if (signUpResponse.hasSucceded)
            {
                //c'è da fare il login da qui?
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", signUpResponse.responseMessage, "Ok");
            }

            IsBusy = false;
        }

    }
}