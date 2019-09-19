using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Services.Annuncio;
using App.Services.Foto;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Models.Image;
using RentIT.Services;
using RentIT.Services.Immagini;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class AggiungiAnnuncioViewModel : BaseViewModel,INotifyPropertyChanged
    {
        IMultiMediaPickerService _multiMediaPickerService;

        public ObservableCollection<MediaFile> Media { get; set; }
        //public ICommand SelectImagesCommand { get; set; }
        //public ICommand SelectVideosCommand { get; set; }

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
        public AggiungiAnnuncioViewModel(INavService navService, FotoService fotoService, AnnuncioService annuncioService, IMultiMediaPickerService multiMediaPickerService) : base(navService)
        {
            _fotoService = fotoService;
            _annuncioService = annuncioService;

            _multiMediaPickerService = multiMediaPickerService;
            /*SelectImagesCommand = new Command(async (obj) =>
            {
                var hasPermission = await CheckPermissionAsync();
                if (hasPermission)
                {
                    Media = new ObservableCollection<MediaFile>();
                    await _multiMediaPickerService.PickPhotosAsync();
                }
            });

            _multiMediaPickerService.OnMediaPicked += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Media.Add(a);
                });
            };*/
        }

        Command _selectImagesCommand;
        public Command SelectImagesCommand
        {
            get
            {
                return _selectImagesCommand
                    ?? (_selectImagesCommand = new Command(async () => await ExecuteSelectImagesCommand()));
            }
        }

        public async Task ExecuteSelectImagesCommand()
        {
            var hasPermission = await CheckPermissionAsync();
            if (hasPermission)
            {
                Media = new ObservableCollection<MediaFile>();
                await _multiMediaPickerService.PickPhotosAsync();
            }

            _multiMediaPickerService.OnMediaPicked += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Media.Add(a);
                });
            };
        }



        async Task<bool> CheckPermissionAsync()
        {
            var retVal = false;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                    {
                        await App.Current.MainPage.DisplayAlert("Attenzione", "Hai bisogno del permesso per accedere alle tue foto.", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
                    status = results[Plugin.Permissions.Abstractions.Permission.Storage];
                }

                if (status == PermissionStatus.Granted)
                {
                    retVal = true;
                     
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Attenzione", "Permesso negato!", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await App.Current.MainPage.DisplayAlert("Attenzine", "Errore", "Ok");
            }

            return retVal;

        }

        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(NomeOggetto) ||
                        string.IsNullOrWhiteSpace(Descrizione) ||
                        (Prezzo == 0) ||
                        string.IsNullOrEmpty(base64);
            return empty;
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

            if (EmptyFields())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Non hai riempito uno o più campi", "Ok");
                return;
            }

            var annuncioRequest = new Ad
            {
                NomeOggetto = NomeOggetto,
                Descrizione = Descrizione,
                Prezzo = Prezzo,
                Data = DateTime.Now
                //AnteprimaImg = base64
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

        public async override Task Init()
        {
            
        }
    }
}

     
