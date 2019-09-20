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
    //prima c'era BaseViewModel<SearchQuery>
    //ora c'è di nuovo >>
    public class AnnunciPageViewModel : BaseViewModel<ObservableCollection<Ad>>
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
        readonly FotoService _fotoService;
        public AnnunciPageViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            Annunci = new ObservableCollection<Ad>();
            _annuncioService = annuncioService;
            _fotoService = fotoService;
        }

        /*SearchQuery _query;
        public SearchQuery Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }*/

        public async override Task Init(ObservableCollection<Ad> annunci)
        {
            Annunci = annunci;
            await LoadImages();
        }

        private async Task LoadImages()
        {
            foreach (var annuncio in Annunci)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
            }
        }

        /*async Task LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            Annunci.Clear();
            Annunci = await _annuncioService.GetLastAds(Query.citta, Query.oggetto);

            IsBusy = false;
        }*/

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

