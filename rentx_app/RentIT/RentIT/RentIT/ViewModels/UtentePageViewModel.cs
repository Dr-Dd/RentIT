
using RentIT.Models.Annuncio;
using RentIT.Models.Immagine;
using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.Annuncio;
using RentIT.Services.Authentication;
using RentIT.Services.Foto;
using RentIT.Services.User;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class UtentePageViewModel : BaseViewModel
    {
        Utente _utente;
        public Utente Utente
        {
            get { return _utente; }
            set
            {
                _utente = value;
                OnPropertyChanged();
            }
        }

        Image _immagine;
        public Image Immagine
        {
            //se _immagine è null, non ci sono foto nel db e ne carica una di default
            get { return _immagine; }
            set
            {
                _immagine = value;
                OnPropertyChanged();
            }
        }

        readonly IAuthenticationService _authService;
        readonly IUserService _userService;
        readonly FotoService _fotoService;
        readonly AnnuncioService _annuncioService;
        public UtentePageViewModel(INavService navService, AuthenticationService authService, UserService userService, FotoService fotoService, AnnuncioService annuncioService) : base(navService)
        {
            _authService = authService;
            _userService = userService;
            _fotoService = fotoService;
            _annuncioService = annuncioService;
            Immagine = new Image();
        }

        public async override Task Init()
        {
            IsBusy = true;
            Utente = await _userService.GetMyProfileAsync();
            Immagine = await getPropic();
            IsBusy = false;
        }

        //Metodo per prendere l'immagine profilo dal database
        public async Task<Image> getPropic()
        {
            ImageModel imgModel = await _fotoService.GetUserImage(Utente.id);
            if (imgModel.data != null)
            {
                Image img = _fotoService.fromStringToImage(imgModel.data);
                return img;
            }
            else
            {
                Image img = new Image();
                img.Source = "userImg.png";  //modificare
                return img;
            }
        }

        //Comando per modificare i dati
        Command _modificaCommand;
        public Command ModificaCommand
        {
            get
            {
                return _modificaCommand
                    ?? (_modificaCommand = new Command(async () => await ExecuteModificaCommandAsync()));
            }
        }

        async Task ExecuteModificaCommandAsync()
        {
            await NavService.NavigateTo<ModificaDatiViewModel>();
        }

        //Comando per visualizzare tutti gli annunci dell'utente
        Command _annunciUtenteCommand;
        public Command AnnunciUtenteCommand
        {
            get
            {
                return _annunciUtenteCommand
                    ?? (_annunciUtenteCommand = new Command(async () => await ExecuteAnnunciUtenteCommandAsync()));
            }
        }

        async Task ExecuteAnnunciUtenteCommandAsync()
        {
            await NavService.NavigateTo<AnnunciUtenteViewModel>();
        }

        //Comando per eseguire il log out
        Command _logOutCommand;
        public Command LogOutCommand
        {
            get
            {
                return _logOutCommand
                    ?? (_logOutCommand = new Command(async () => await ExecuteLogOutCommandAsync()));
            }
        }

        async Task ExecuteLogOutCommandAsync()
        {
            IsBusy = true;

            var response = await _authService.LogoutAsync();
            if (response)
            {
                await App.Current.MainPage.DisplayAlert("RentIT", "A presto!", "Ok");
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Impossibile effettuare il log out", "Ok");
            }

            IsBusy = false;

        }
    }
}
