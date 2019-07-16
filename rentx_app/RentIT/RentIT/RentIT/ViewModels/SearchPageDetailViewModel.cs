using System;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Models.User;
using RentIT.Services;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class SearchPageDetailViewModel : BaseViewModel
    {
        public SearchPageDetailViewModel(INavService navService) : base(navService)
        {
        }
 
        Command _aggiungiAnnuncioCommand;
        public Command AggiungiAnnuncioCommand
        {
            get
            {
                return _aggiungiAnnuncioCommand
                    ?? (_aggiungiAnnuncioCommand = new Command(async () => await ExecuteAddAnnuncioCommandAsync()));
            }
        }

        //Riaggiungere utente quando sarà implementato il login e la gestione degli utenti
        async Task ExecuteAddAnnuncioCommandAsync()
        {
            
            await NavService.NavigateTo<AggiungiAnnuncioPageViewModel>();

            //Commentato solo per visualizzare la pagina e vedere se è corretta
            //Molto probabilmente verrà usato il codice qui sotto

            /*if (AppSettings.AccessToken.Equals(String.Empty))
            {
                await NavService.NavigateTo<LoginPageViewModel>();
            }
            else
            {
                // TODO: da implementare quando si avrà una "AddAnnuncioPage"
                /* await NavService.NavigateTo<AddAnnuncioViewModel, Utente>(utente);
             }*/
            
        }

        Command<SearchQuery> _search;
        public Command<SearchQuery> Search
        {
            get
            {
                return _search
                    ?? (new Command<SearchQuery>(async (query) => await ExecuteSearchCommand(query)));
            }
        }

        private async Task ExecuteSearchCommand(SearchQuery query)
        {
            await NavService.NavigateTo<AnnunciPageViewModel, SearchQuery>(query);
        }

        public async override Task Init()
        {
        }
    }
}