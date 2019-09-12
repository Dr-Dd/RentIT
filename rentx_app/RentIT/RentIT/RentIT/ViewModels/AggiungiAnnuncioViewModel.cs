using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using App.Services.Annuncio;
using App.Services.Foto;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class AggiungiAnnuncioViewModel : BaseViewModel
    {
        //Queste probabilmente saranno una lista
        Image _immagine;
        public Image Immagine
        {
            get { return _immagine ?? (new Image()); }
            set
            {
                _immagine = value;
                OnPropertyChanged();
            }
        }

        string _nomeOggetto;
        public string NomeOggetto
        {
            get { return _nomeOggetto; }
            set
            {
                _nomeOggetto = value;
                OnPropertyChanged();
            }
        }

        string _descrizione;
        public string Descrizione
        {
            get { return _descrizione; }
            set
            {
                _descrizione = value;
                OnPropertyChanged();
            }
        }

        decimal _prezzo;
        public decimal Prezzo
        {
            get { return _prezzo; }
            set
            {
                _prezzo = value;
                OnPropertyChanged();
            }
        }

        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;
        public AggiungiAnnuncioViewModel(INavService navService, FotoService fotoService, AnnuncioService annuncioService) : base(navService)
        {
            _fotoService = fotoService;
            _annuncioService = annuncioService;
        }

        public async override Task Init()
        {
        }

        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(NomeOggetto) ||
                        string.IsNullOrWhiteSpace(Descrizione) ||
                        (Prezzo == 0);
            return empty;
        }

        //Comando per aggiungere foto all'annuncio
        Command _aggiungiFotoCommand;
        public Command AggiungiFotoCommand
        {
            get
            {
                return _aggiungiFotoCommand
                    ?? (_aggiungiFotoCommand = new Command(async () => await ExecuteAggiungiFotoCommand()));
            }
        }

        async Task ExecuteAggiungiFotoCommand()
        {
            //Caricare un'immagine dalla galleria
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                //se esiste, si salva nel db associato all'annuncio
                string base64 = _fotoService.fromStreamToString(stream);

                //questa riga serve solo a visualizzare l'immagine in attesa del collegamento al db
                Immagine = _fotoService.fromStringToImage(base64);

                //tipo _annuncioService.UploadItemImageAsync(base64)
            }
        }

        //Comando per salvare l'annuncio
        Command _aggiungiAnnuncioCommand;
        public Command AggiungiAnnuncioCommand
        {
            get
            {
                return _aggiungiAnnuncioCommand
                    ?? (_aggiungiAnnuncioCommand = new Command(async () => await ExecuteAggiungiAnnuncioCommand()));
            }
        }

        async Task ExecuteAggiungiAnnuncioCommand()
        {
            IsBusy = true;

            if(EmptyFields()){
                await App.Current.MainPage.DisplayAlert("Errore", "Non hai riempito uno o più campi", "Ok");
                return;
            }

            var annuncioRequest = new Ad
            {
                NomeOggetto = NomeOggetto,
                Descrizione = Descrizione,
                Prezzo = Prezzo,
                Data = DateTime.Now
            };
            var response = await _annuncioService.AddAnnuncioAsync(annuncioRequest);
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
    }
}
