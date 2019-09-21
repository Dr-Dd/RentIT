using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using RentIT.Services.Foto;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System;

namespace RentIT.ViewModels
{
    public class GestioneAnnuncioViewModel : BaseViewModel<Ad>
    {
        Ad _annuncio;
        public Ad Annuncio
        {
            get
            {
                return _annuncio;
            }
            set
            {
                _annuncio = value;
                OnPropertyChanged();
            }
        }

        List<Image> _immagini;
        public List<Image> Immagini
        {
            get
            {
                return _immagini;
            }
            set
            {
                _immagini = value;
                OnPropertyChanged();
            }
        }

        bool _isPrenotato;
        public bool IsPrenotato
        {
            get
            {
                return _isPrenotato;
            }

            set
            {
                _isPrenotato = value;
                OnPropertyChanged();
            }
        }

        bool _isNotPrenotato;
        public bool IsNotPrenotato
        {
            get
            {
                return _isNotPrenotato;
            }

            set
            {
                _isNotPrenotato = value;
                OnPropertyChanged();
            }
        }

        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;
        public GestioneAnnuncioViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
            Immagini = new List<Image>();
        }

        public async override Task Init(Ad annuncio)
        {
            Annuncio = annuncio;

            Immagini = await LoadAdImages();
            
            // setto lo stato di prenotazione dell'annuncio
            IsPrenotato = await _annuncioService.isAdBooked(Annuncio.id);
            IsNotPrenotato = !IsPrenotato;
        }

        public async Task<List<Image>> LoadAdImages()
        {
            var listaImagesModel = await _fotoService.GetAdImagesAsync(Annuncio.id);
            var listaImages = new List<Image>();

            // spacchetto tutto...
            foreach (var imgModel in listaImagesModel)
            {
                // e inserisco fra le immagini dell'annuncio
                listaImages.Add(_fotoService.fromStringToImage(imgModel.data));
            }

            return listaImages;
        }

        /*Comando per eliminare l'annuncio*/
        Command _eliminaAnnuncioCommand;
        public Command EliminaAnnuncioCommand
        {
            get
            {
                return _eliminaAnnuncioCommand
                    ?? (_eliminaAnnuncioCommand = new Command(async () => await ExecuteEliminaAnnuncioCommand()));
            }
        }

        async Task ExecuteEliminaAnnuncioCommand()
        {
            IsBusy = true;
            
            var response = await _annuncioService.DeleteAdAsync(Annuncio.id);
            if (response.hasSucceded)
            {
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }

            IsBusy = false;
        }

        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(Annuncio.nomeOggetto) ||
                        string.IsNullOrWhiteSpace(Annuncio.descrizione) ||
                        (Annuncio.prezzo == 0);
            return empty;
        }

        /*Comando per modificare l'annuncio*/
        Command _modificaAnnuncioCommand;
        public Command ModificaAnnuncioCommand
        {
            get
            {
                return _modificaAnnuncioCommand
                    ?? (_modificaAnnuncioCommand = new Command(async () => await ExecuteModificaAnnuncioCommand()));
            }
        }

        async Task ExecuteModificaAnnuncioCommand()
        {
            IsBusy = true;

            if (EmptyFields())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Non hai riempito uno o più campi", "Ok");
                return;
            }

            Annuncio.nomeOggetto = Annuncio.nomeOggetto.Trim();
            Annuncio.descrizione = Annuncio.descrizione.Trim();

            var response = await _annuncioService.ModifyAdDataAsync(Annuncio);
            if (response.hasSucceded)
            {
                StringBuilder successo = new StringBuilder();
                successo.Append("Annuncio modificato con successo!");
                await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "ok");
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }
            
            IsBusy = false;
        }

        /*Comando per prenotare l'annuncio*/
        Command _prenotaAnnuncioCommand;
        public Command PrenotaAnnuncioCommand
        {
            get
            {
                return _prenotaAnnuncioCommand
                    ?? (_prenotaAnnuncioCommand = new Command(async () => await ExecutePrenotaAnnuncioCommand()));
            }
        }

        async Task ExecutePrenotaAnnuncioCommand()
        {
            IsBusy = true;

            await _annuncioService.prenotaAd(Annuncio.id);
            StringBuilder successo = new StringBuilder();
            successo.Append("Annuncio prenotato con successo!");
            await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "ok");
            await NavService.NavigateToMainPage();

            IsBusy = false;
        }

        /*Comando per liberare l'annuncio prenotato*/
        Command _liberaAnnuncioCommand;
        public Command LiberaAnnuncioCommand
        {
            get
            {
                return _liberaAnnuncioCommand
                    ?? (_liberaAnnuncioCommand = new Command(async () => await ExecuteLiberaAnnuncioCommand()));
            }
        }

        async Task ExecuteLiberaAnnuncioCommand()
        {
            IsBusy = true;

            await _annuncioService.liberaAd(Annuncio.id);
            StringBuilder successo = new StringBuilder();
            successo.Append("Annuncio liberato con successo!");
            await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "ok");
            await NavService.NavigateToMainPage();

            IsBusy = false;
        }
    }
}
