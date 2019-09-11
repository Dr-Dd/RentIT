using RentIT.Models;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class GestioneAnnuncioViewModel : BaseViewModel<Annuncio>
    {
        Annuncio _annuncio;
        public Annuncio Annuncio
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

        public GestioneAnnuncioViewModel(INavService navService) : base(navService)
        {
        }

        public async override Task Init(Annuncio annuncio)
        {
            Annuncio = annuncio;
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
            /*
            var response = await _annuncioService.Delete();
            if (response.hasSucceded)
            {
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }
            */

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

            /*
            var response = await _annuncioService.ModifyItemData(Annuncio);
            if (response.hasSucceded)
            {
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }
            */
            IsBusy = false;
        }
    }
}
