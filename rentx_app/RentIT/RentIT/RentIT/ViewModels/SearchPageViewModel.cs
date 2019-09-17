using App.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using RentIT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
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
        public SearchPageViewModel(INavService navService, AnnuncioService annuncioService) : base(navService)
        {
            _annuncioService = annuncioService;
        }

        public override async Task Init()
        {
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
        Command _annunciPageCommand;
        public Command AnnunciPageCommand
        {
            get
            {
                return _annunciPageCommand ?? (
                    _annunciPageCommand = new Command(async () => await ExecuteAnnunciPageCommand()));
            }
        }

        async Task ExecuteAnnunciPageCommand()
        {
            await NavService.NavigateTo<AnnunciPageViewModel>();
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

        async Task ExecuteCercaCommand()
        {
            List<Ad> risultati = await _annuncioService.GetLastAds(Citta, Oggetto);
            await NavService.NavigateTo<AnnunciPageViewModel, List<Ad>>(risultati);
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
            if(!IsLogged())
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
    }
}