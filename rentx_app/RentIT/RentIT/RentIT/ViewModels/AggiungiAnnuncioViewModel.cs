using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using App.Services.Annuncio;
using App.Services.Foto;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Models.Immagine;
using RentIT.Services;
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

        ObservableCollection<ElementoListaImmagine> _immagini;
        public ObservableCollection<ElementoListaImmagine> Immagini
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
        string base64;

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
            Immagini = new ObservableCollection<ElementoListaImmagine>();
        }

        public async override Task Init()
        {
        }

        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(NomeOggetto) ||
                        string.IsNullOrWhiteSpace(Descrizione) ||
                        (Prezzo == 0) ||
                        string.IsNullOrEmpty(base64);
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
                //MemoryStream ms = new MemoryStream();
                //stream.CopyTo(ms);
                //byte[] arrayImmagine = ms.ToArray();

                //se esiste, si salva nel db associato all'annuncio
                string base64 = _fotoService.fromStreamToString(stream);
                //Image immagineTemp = new Image();
                //immagineTemp.Source = ImageSource.FromStream(() => new MemoryStream(arrayImmagine));

                //questa riga serve solo a visualizzare l'immagine in attesa del collegamento al db
                //Image immagineContainer = new Image();
                //Immagine = _fotoService.fromStringToImage(base64);
                //immagineContainer.Source = Immagine.Source;
                ElementoListaImmagine elemLista = new ElementoListaImmagine();
                byte[] fromBase64ToByte = Convert.FromBase64String(base64);
                elemLista.SorgenteImmagine = ImageSource.FromStream(() => new MemoryStream(fromBase64ToByte));
                Immagini.Add(elemLista);
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
                NomeOggetto = NomeOggetto,
                Descrizione = Descrizione,
                Prezzo = Prezzo,
                Data = DateTime.Now,
                AnteprimaImg = base64
            };
            var response = await _annuncioService.AddAnnuncioAsync(annuncioRequest);
            if (response.hasSucceded)
            {
                //per salvare la foto serve IDannuncio
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
