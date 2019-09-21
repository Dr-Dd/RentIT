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
    public class AnnunciPrenotatiViewModel : BaseViewModel
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

        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;
        public AnnunciPrenotatiViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
            AnnunciPrenotati = new ObservableCollection<Ad>();
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

        async Task LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            AnnunciPrenotati.Clear();
            AnnunciPrenotati = await _annuncioService.GetMyBookedAds();

            // Carico le immagini...
            foreach (var annuncio in AnnunciPrenotati)
            {
                annuncio.anteprimaImgXam = _fotoService.fromStringToImage(annuncio.anteprimaImg);
                Console.WriteLine("[DEBUG-AnnunciAttivi] Annuncio nome: " + annuncio.nomeOggetto);
                Console.WriteLine("[DEBUG-AnnunciAttivi] Annuncio descrizione: " + annuncio.descrizione);
            }

            IsBusy = false;
        }
    }
}
