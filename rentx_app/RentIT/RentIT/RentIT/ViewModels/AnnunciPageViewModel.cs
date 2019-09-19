using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services;
using Xamarin.Forms;
using RentIT.Models.User;
using RentIT.Models.Annuncio;
using RentIT.Services.Annuncio;

namespace RentIT.ViewModels
{
    //prima c'era BaseViewModel<SearchQuery>
    //ora c'è di nuovo >>
    public class AnnunciPageViewModel : BaseViewModel<SearchQuery>
    {

        ObservableCollection<Ad> _annunci;
        public ObservableCollection<Ad> Annunci
        {
            get { return _annunci; }
            set
            {
                _annunci = value;
                OnPropertyChanged();
            }
        }

        readonly IAnnuncioService _annuncioService;
        public AnnunciPageViewModel(INavService navService, AnnuncioService annuncioService) : base(navService)
        {
            Annunci = new ObservableCollection<Ad>();
            _annuncioService = annuncioService;
        }

        SearchQuery _query;
        public SearchQuery Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }

        public async override Task Init(SearchQuery query)
        {
            // TODO: Implementare la ricerca
            Query = query;
            await LoadEntries();
        }

        async Task LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            Annunci.Clear();
            Annunci = await _annuncioService.GetLastAds(Query.citta, Query.oggetto);

            IsBusy = false;
        }

        Command<Ad> _viewAnnuncio;
        public Command<Ad> ViewAnnuncio
        {
            get
            {
                return _viewAnnuncio
                    ?? (new Command<Ad>(async (annuncio) => await ExecuteViewAnnuncio(annuncio)));
            }
        }

        async Task ExecuteViewAnnuncio(Ad annuncio)
        {
            await NavService.NavigateTo<DettaglioAnnuncioViewModel, Ad>(annuncio);
        }
    }
}

