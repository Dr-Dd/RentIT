using App.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        ObservableCollection<OggettoImmagine> _oggettiImmagine;
        public ObservableCollection<OggettoImmagine> OggettiImmagine
        {
            get
            {
                return _oggettiImmagine;
            }
            set
            {
                _oggettiImmagine = value;
                OnPropertyChanged();
            }
        }


        readonly IAnnuncioService _annuncioService;
        public GestioneAnnuncioViewModel(INavService navService, AnnuncioService annuncioService) : base(navService)
        {
            _annuncioService = annuncioService;
        }

        public async override Task Init(Ad annuncio)
        {
            Annuncio = annuncio;
            OggettiImmagine = creaOggettiImmagine(Annuncio);
        }

        public ObservableCollection<OggettoImmagine> creaOggettiImmagine(Ad annuncio)
        {
            OggettiImmagine = new ObservableCollection<OggettoImmagine>();
            foreach (FileImageSource image in Annuncio.PercorsiImmagine)
            {
                OggettiImmagine.Add(new OggettoImmagine()
                {
                    Immagine = image
                });
            }
            return OggettiImmagine;
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
            
            var response = await _annuncioService.DeleteAdAsync(Annuncio.Id);
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
            var empty = string.IsNullOrWhiteSpace(Annuncio.NomeOggetto) ||
                        string.IsNullOrWhiteSpace(Annuncio.Descrizione) ||
                        (Annuncio.Prezzo == 0);
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

            if (!EmptyFields())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Non hai riempito uno o più campi", "Ok");
                return;
            }
            
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
    }
}
