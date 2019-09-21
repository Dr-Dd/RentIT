using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RentIT.Services;
using Xamarin.Forms;
using RentIT.Models.Annuncio;
using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Services.Foto;
using System;

namespace RentIT.ViewModels
{
    public class AnnunciQueryViewModel : BaseViewModel<SearchQuery>
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

        /*Questa proprietà serve esclusivamente a visualizzare i campi nel titolo*/
        string _messaggioTitolo;
        public string MessaggioTitolo
        {
            get
            {
                return _messaggioTitolo;
            }
            set
            {
                _messaggioTitolo = value;
                OnPropertyChanged();
            }
        }

        bool _nessunRisultato;
        public bool NessunRisultato
        {
            get { return _nessunRisultato; }
            set
            {
                _nessunRisultato = value;
                OnPropertyChanged();
            }
        }

        readonly IAnnuncioService _annuncioService;
        readonly FotoService _fotoService;
        public AnnunciQueryViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
            Annunci = new ObservableCollection<Ad>();
        }
        
        public async override Task Init(SearchQuery query)
        {
            IsBusy = true;
            Annunci.Clear();
            Query = query;
            NessunRisultato = false;
            await LoadEntries();
            MessaggioTitolo = String.Format("{0} a {1}", Query.oggetto, Query.citta);
            IsBusy = false;
        }

        private async Task LoadEntries()
        {
            Annunci.Clear();
            Annunci = await _annuncioService.GetLastAds(Query.citta, Query.oggetto);
            if (Annunci.Count == 0)
            {
                NessunRisultato = true;
            }
            else
                await LoadImages();
        }

        private async Task LoadImages()
        {
            foreach (var annuncio in Annunci)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
            }
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
