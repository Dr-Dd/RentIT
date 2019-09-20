using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using RentIT.Services.Foto;

namespace RentIT.ViewModels
{
    public class AnnunciUtenteViewModel : BaseViewModel
    {

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

        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;
        public AnnunciUtenteViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
            AnnunciNonPrenotati = new ObservableCollection<Ad>();
            AnnunciPrenotati = new ObservableCollection<Ad>();
        }

        public async override Task Init()
        {
            await LoadEntries();
        }

        /**
        * IMPORTANTE: Nello stato attuale, la ListView fa laggare
        * vistosamente l'app, trovare un modo di rendere più veloce
        * ed efficiente lo scroll
        */

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

        async Task LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            AnnunciPrenotati.Clear();
            AnnunciNonPrenotati.Clear();

            AnnunciPrenotati = await _annuncioService.GetMyBookedAds();
            AnnunciNonPrenotati = await _annuncioService.GetMyNotBookedAds();

            // Carico le immagini...
            foreach (var annuncio in AnnunciPrenotati)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
            }

            // Stessa cosa per quelli non prenotati
            foreach (var annuncio in AnnunciNonPrenotati)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
            }

            IsBusy = false;
        }
    }
}
