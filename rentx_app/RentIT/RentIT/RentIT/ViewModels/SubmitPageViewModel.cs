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
            }
        }

        public override async Task Init()
        {
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