using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using RentIT.Services.Foto;
using System;

namespace RentIT.ViewModels
{
    public class AnnunciUtenteViewModel : BaseViewModel<ObservableCollection<Ad>>
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

        public async override Task Init(ObservableCollection<Ad> annunciNonPrenotati)
        {
            AnnunciNonPrenotati = annunciNonPrenotati;

            await LoadEntries();

            foreach (var annuncio in AnnunciNonPrenotati)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
                Console.WriteLine("[DEBUG-AnnunciUtente] Annuncio nome: " + annuncio.nomeOggetto);
                Console.WriteLine("[DEBUG-AnnunciUtente] Annuncio descrizione: " + annuncio.descrizione);
            }

            foreach (var annuncio in AnnunciPrenotati)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
                Console.WriteLine("[DEBUG-AnnunciUtente] Annuncio nome: " + annuncio.nomeOggetto);
                Console.WriteLine("[DEBUG-AnnunciUtente] Annuncio descrizione: " + annuncio.descrizione);
            }
        }

        async Task LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            AnnunciPrenotati.Clear();
            AnnunciPrenotati =  await _annuncioService.GetMyBookedAds();

            IsBusy = false;
        }
    }
}
