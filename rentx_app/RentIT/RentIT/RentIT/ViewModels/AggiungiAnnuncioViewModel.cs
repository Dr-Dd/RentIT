using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RentIT.Models.Annuncio;
using RentIT.Models.Immagine;
using RentIT.Services;
using RentIT.Services.Annuncio;
using RentIT.Services.Foto;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class AggiungiAnnuncioViewModel : BaseViewModel
    {

        Image _immagine;
        public Image Immagine
        {
            get { return _immagine; }
            set
            {
                _immagine = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Image> _immagini;
        public ObservableCollection<Image> Immagini
        {
            get { return _immagini; }
            set
            {
                _immagini = value;
                OnPropertyChanged();
            }
        }
        /* Questa stringa serve per mantenere il riferimento all'immagine
         * in quanto, dopo che viene caricata, viene subito visualizzata
         */
        List<string> listaImmaginiInBase64;

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
            Immagini = new ObservableCollection<Image>();
            listaImmaginiInBase64 = new List<string>();
        }

        public async override Task Init()
        {
        }

        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(NomeOggetto) ||
                        string.IsNullOrWhiteSpace(Descrizione) ||
                        (Prezzo == 0) ||
                        (Immagini.Count == 0);
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
                string immagineInBase64 = _fotoService.fromStreamToString(stream);
                // Lista di stringhe da inviare al db nel salvataggio dell'annuncio
                listaImmaginiInBase64.Add(immagineInBase64);

                // NB: fromStringToImage setta già la source di image
                Image img = new Image();
                img = _fotoService.fromStringToImage(immagineInBase64);
                Immagini.Add(img);
            }
            else
            {
                System.Console.WriteLine("Mannaggia");
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
                nomeOggetto = NomeOggetto,
                descrizione = Descrizione,
                prezzo = Prezzo,
                data = DateTime.Now,
                anteprimaImg = listaImmaginiInBase64[0]
            };
            var response = await _annuncioService.AddAnnuncioAsync(annuncioRequest);
            if (response.hasSucceded)
            {
                // Per ogni stringa immagine in base 64 caricata...
                foreach (var imgB64 in listaImmaginiInBase64) {
                    // La invio al db con l'id del nuovo annuncio appena generato!!
                    await _fotoService.UploadAdImagesAsync(response.id, imgB64);
                }

                StringBuilder successo = new StringBuilder();
                successo.AppendLine("Annuncio aggiunto con successo!");
                successo.AppendLine("Puoi trovarlo nella sezione 'I MIEI ANNUNCI' sul tuo profilo");
                await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "Ok");
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
