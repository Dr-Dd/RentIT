﻿
using App.Services.Foto;
using RentIT.Models.Immagine;
using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.Authentication;
using RentIT.Services.Foto;
using RentIT.Services.User;
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
            get { return _immagine ?? (new Image { Source = "meme.png" }); }
            set
            {
                _immagine = value;
                OnPropertyChanged();
            }
        }

        readonly IAuthenticationService _authService;
        readonly IUserService _userService;
        readonly FotoService _fotoService;
        public UtentePageViewModel(INavService navService, AuthenticationService authService, UserService userService, FotoService fotoService) : base(navService)
        {
            _authService = authService;
            _userService = userService;
            _fotoService = fotoService;
        }

        public async override Task Init()
        {
            Utente = await _userService.GetMyProfileAsync();
            //TODO: da decommentare dopo fatto il collegamento al db
            //Immagine = await getPropic();
        }

        //Metodo per prendere l'immagine profilo dal database
        public async Task<Image> getPropic()
        {
            ImageModel foto = await _fotoService.GetUserImage(AppSettings.UserId);
            Image img = null;
            if(foto != null)
                img = _fotoService.fromStringToImage(foto.data);
            return img;
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
        //da implementare
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
                // Dovrebbe essere più corretta la soluzione sotto, testiamola
                //await NavService.NavigateToMainPage();
                await NavService.NavigateTo<SearchPageViewModel>();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Impossibile effettuare il log out", "Ok");
            }

            IsBusy = false;

        }
    }
}
