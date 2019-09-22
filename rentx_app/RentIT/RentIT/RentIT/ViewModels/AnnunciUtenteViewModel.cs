using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using RentIT.Services.Foto;
using System;
using RentIT.Models.User;

namespace RentIT.ViewModels
{
    public class AnnunciUtenteViewModel : BaseViewModel
    {
        ObservableCollection<Ad> _annunciNonPrenotati;
        public ObservableCollection<Ad> AnnunciNonPrenotati
        {
            get { return _annunciNonPrenotati; }
            set
            {
                _annunciNonPrenotati = value;
                OnPropertyChanged();
            }
        }

        bool _hasAnnunciNonPrenotatiVar;
        public bool HasNotAnnunciNonPrenotatiVar
        {
            get { return _hasAnnunciNonPrenotatiVar; }
            set
            {
                _hasAnnunciNonPrenotatiVar = value;
                OnPropertyChanged();
            }
        }

        bool _hasAnnunciPrenotatiVar;
        public bool HasNotAnnunciPrenotatiVar
        {
            get { return _hasAnnunciPrenotatiVar; }
            set
            {
                _hasAnnunciPrenotatiVar = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Ad> _annunciPrenotati;
        public ObservableCollection<Ad> AnnunciPrenotati
        {
            get { return _annunciPrenotati; }
            set
            {
                _annunciPrenotati = value;
                OnPropertyChanged();
            }
        }
        
        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;
        public AnnunciUtenteViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
            AnnunciPrenotati = new ObservableCollection<Ad>();
            AnnunciNonPrenotati = new ObservableCollection<Ad>();
        }

        public async override Task Init()
        {
            IsBusy = true;

            AnnunciNonPrenotati.Clear();
            AnnunciNonPrenotati = await _annuncioService.GetMyNotBookedAds();
            if(AnnunciNonPrenotati.Count == 0)
            {
                HasNotAnnunciNonPrenotatiVar = true;
            } else
            {
                HasNotAnnunciNonPrenotatiVar = false;
            }
            await LoadImages(AnnunciNonPrenotati);

            AnnunciPrenotati.Clear();
            AnnunciPrenotati = await _annuncioService.GetMyBookedAds();
            if(AnnunciPrenotati.Count == 0)
            {
                HasNotAnnunciPrenotatiVar = true;
            } else
            {
                HasNotAnnunciPrenotatiVar = false;
            }
            await LoadImages(AnnunciPrenotati);

            IsBusy = false;
        }

        private async Task LoadImages(ObservableCollection<Ad> Annunci)
        {
            foreach (var annuncio in Annunci)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
            }
        }

        Command<Ad> _viewGestioneAnnuncio;
        public Command<Ad> ViewGestioneAnnuncio
        {
            get
            {
                return _viewGestioneAnnuncio
                    ?? (new Command<Ad>(async (annuncio) => await ExecuteViewGestioneAnnuncio(annuncio)));
            }
        }

        async Task ExecuteViewGestioneAnnuncio(Ad annuncio)
        {
            await NavService.NavigateTo<GestioneAnnuncioViewModel, Ad>(annuncio);
        }
    }
}
