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

        Command<Utente> _addAnnuncio;
        public Command<Utente> AddAnnuncio
        {
            get
            {
                return _addAnnuncio
                    ?? (new Command<Utente>(async (utente) => await ExecuteAddAnnuncioCommand(utente)));
            }
        }

        async Task ExecuteAddAnnuncioCommand(Utente utente)
        {
            if (AppSettings.AccessToken.Equals(String.Empty))
            {
                await NavService.NavigateTo<LoginPageViewModel>();
            }
            else
            {
                // TODO: da implementare quando si avrà una "AddAnnuncioPage"
                /* await NavService.NavigateTo<AddAnnuncioViewModel, Utente>(utente); */
            }
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