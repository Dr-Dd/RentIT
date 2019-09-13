using RentIT.Models;
using RentIT.Services;
using RentIT.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {

        public SearchPageViewModel(INavService navService) : base(navService)
        {
        }

        public override async Task Init()
        {
        }

        bool IsLogged()
        {
            return !AppSettings.AccessToken.Equals(string.Empty);
        }
        //comando nella toolbar
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

        //comandi nel menù a tendina
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

        //pulsante aggiungi annuncio
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
            await NavService.NavigateTo<AggiungiAnnuncioViewModel>();
        }
    }
}