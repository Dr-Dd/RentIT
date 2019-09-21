
using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Models.Immagine;
using RentIT.Services;
using RentIT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using RentIT.Services.User;
using RentIT.Models.User;
using RentIT.Services.Authentication;

namespace RentIT.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {

        List<string> _listaCitta;
        public List<string> ListaCitta
        {
            get { return _listaCitta; }
            set
            {
                _listaCitta = value;
                OnPropertyChanged();
            }
        }

        string _oggetto;
        public string Oggetto
        {
            get { return _oggetto; }
            set
            {
                _oggetto = value;
                OnPropertyChanged();
            }
        }

        string _titolo;
        public string Titolo
        {
            get { return _titolo; }
            set
            {
                _titolo = value;
                OnPropertyChanged();
            }
        }

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

        string _citta;
        public string Citta
        {
            get { return _citta; }
            set
            {
                _citta = value;
                OnPropertyChanged();
            }
        }

        readonly IAnnuncioService _annuncioService;
        readonly IUserService _userService;
        readonly IAuthenticationService _authService;
        public SearchPageViewModel(INavService navService, AnnuncioService annuncioService, UserService userService, AuthenticationService authService) : base(navService)
        {
            _userService = userService;
            _annuncioService = annuncioService;
            _authService = authService;
            ListaCitta = MiscCostants.tutteCitta;
        }

        bool _isLoggedVar;
        public bool IsLoggedVar
        {
            get { return _isLoggedVar; }
            set
            {
                _isLoggedVar = value;
                OnPropertyChanged();
            }
        }

        bool _isNotLoggedVar;
        public bool IsNotLoggedVar
        {
            get { return _isNotLoggedVar; }
            set
            {
                _isNotLoggedVar = value;
                OnPropertyChanged();
            }
        }

        bool IsLogged()
        {
            return !AppSettings.AccessToken.Equals(string.Empty);
        }

        /*
         * Comando nella toolbar
         */
        Command _loginCommand;
        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (
                    _loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }

        async Task ExecuteLoginCommand()
        {
            if (!IsLogged())
            {
                await NavService.NavigateTo<LoginPageViewModel>();
            }
            else //apre la pagina utente se già loggato
            {
                await NavService.NavigateTo<UtentePageViewModel>();
            }
        }

        /* 
         * Comandi nel menù a tendina
         */
        Command _annunciPageCommand; // Momentaneamente commentato, da discutere la possibilità di dividere la view degli annunci e la view degli annunci utente 
        public Command AnnunciPageCommand
        {
            get
            {
                return _annunciPageCommand ?? (
                    _annunciPageCommand = new Command(async () => await ExecuteAnnunciPageCommand()));
            }
        }

        // Momentaneamente commentato 
        async Task ExecuteAnnunciPageCommand()
        {

            ObservableCollection<Ad> Annunci = await _annuncioService.GetLastAds(Citta, Oggetto);

            await NavService.NavigateTo<AnnunciPageViewModel, ObservableCollection<Ad>>(Annunci);
        }


        Command _submitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _submitCommand ?? (
                    _submitCommand = new Command(async () => await ExecuteSubmitCommand()));
            }
        }


        async Task ExecuteSubmitCommand()
        {
            await NavService.NavigateTo<SubmitPageViewModel>();
        }

        Command _utenteCommand;
        public Command UtenteCommand
        {
            get
            {
                return _utenteCommand ?? (
                    _utenteCommand = new Command(async () => await ExecuteUtenteCommand()));
            }
        }


        async Task ExecuteUtenteCommand()
        {
            await NavService.NavigateTo<UtentePageViewModel>();
        }

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
                await NavService.NavigateTo<SearchPageViewModel>();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Impossibile effettuare il log out", "Ok");
            }

            IsBusy = false;

        }


        /*
         * Pulsante cerca
         */
        Command _cercaCommand;
        public Command CercaCommand
        {
            get
            {
                return _cercaCommand ?? (
                    _cercaCommand = new Command(async () => await ExecuteCercaCommand()));
            }
        }

        bool isEmpty()
        {
            return (string.IsNullOrWhiteSpace(Oggetto)) || (string.IsNullOrWhiteSpace(Citta));
        }

        async Task ExecuteCercaCommand()
        {
            if (isEmpty())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Riempi entrambi i campi di ricerca", "Ok");
                return;
            }
            SearchQuery query = new SearchQuery();
            query.citta = Citta.Trim();
            query.oggetto = Oggetto.Trim();
            await NavService.NavigateTo<AnnunciQueryViewModel, SearchQuery>(query);
        }

        /*
         * Pulsante aggiungi annuncio
         */
        Command _addAnnuncio;
        public Command AddAnnuncio
        {
            get
            {
                return _addAnnuncio
                    ?? (_addAnnuncio = new Command(async () => await ExecuteAddAnnuncioCommandAsync()));
            }
        }


        async Task ExecuteAddAnnuncioCommandAsync()
        {
            if (!IsLogged())
            {
                await App.Current.MainPage.DisplayAlert("RentIT", "Iscriviti o effettua il login per aggiungere un annuncio!", "Ok");
                await NavService.NavigateTo<LoginPageViewModel>();
                return;
            }
            if (AppSettings.NewProfile)
            {
                await App.Current.MainPage.DisplayAlert("RentIT", "Aggiorna i tuoi dati prima di aggiungere un annuncio!", "Ok");
                await NavService.NavigateTo<ModificaDatiViewModel>();
                return;
            }
            await NavService.NavigateTo<AggiungiAnnuncioViewModel>();
        }

        public async override Task Init()
        {
            IsLoggedVar = IsLogged();
            IsNotLoggedVar = !IsLoggedVar;
            if (IsLoggedVar)
            {
                Utente = await _userService.GetMyProfileAsync();
                Titolo = "Benvenuto " + Utente.name + "!";
            }
        }
    }
}
