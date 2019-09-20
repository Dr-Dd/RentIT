using RentIT.Models.Annuncio;
using RentIT.Services;
using RentIT.Services.Annuncio;
using RentIT.Services.Foto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class AnnunciAttiviViewModel : BaseViewModel
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

        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;
        public AnnunciAttiviViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
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

        public async override Task Init()
        {
            await LoadEntries();
        }

        public async Task LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            AnnunciNonPrenotati.Clear();
            AnnunciNonPrenotati = await _annuncioService.GetMyNotBookedAds();

            // Stessa cosa per quelli non prenotati
            foreach (var annuncio in AnnunciNonPrenotati)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
            }

            IsBusy = false;
        }
    }
}
