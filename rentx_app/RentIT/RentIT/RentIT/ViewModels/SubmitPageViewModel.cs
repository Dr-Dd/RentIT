using System.Threading.Tasks;
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

        // Parametro a cui lego la visibilità password, che verrà settato dal validatore di quest'ultima
        bool _isPwErrorVisible;
        public bool IsPwErrorVisible
        {
            get { return _isPwErrorVisible; }
            set
            {
                _isPwErrorVisible = value;
                OnPropertyChanged();                        // 
            }                                               //
        }                                                   //

        string _confermaPassword;
        public string ConfermaPassword
        {
            get { return _confermaPassword; }
            set
            {
                _confermaPassword = value;
                OnPropertyChanged();
                SignInCommand.ChangeCanExecute(); // Nota come, nel viewModel, la funzione di set
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
         * essa va associata a un comando per permettere questa interattività (vedi SignInCommand). Nota come inoltre
         * da essa setto anche la visibilità dei messaggi di errore (IsPwErrorVisible), poiché chiaramente esso sarà visibile
         * solo se le password sono errate!!
         */
        bool PasswordsAreTheSameAndNotEmpty()
        {
            var isValid = (Password == ConfermaPassword && 
                !string.IsNullOrWhiteSpace(Password));

            if (isValid) IsPwErrorVisible = false;
            else IsPwErrorVisible = true;

            return isValid;
        }

        /*comando di registrazione*/
        Command _signInCommand;
        public Command SignInCommand
        {
            get
            {
                return _signInCommand // Piccola nota sul comando, come puoi vedere accetta un booleano che controlla l'eseguibilità del comando, in questo caso una funzione
                    ?? (_signInCommand = new Command(async () => await ExecuteSignInCommand(), PasswordsAreTheSameAndNotEmpty));
            }
        }
        
        async Task ExecuteSignInCommand()
        {
            IsBusy = true;
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