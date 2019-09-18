using App.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Models.User;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


//Da modificare
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
            get { return _annunciPrenotati; }
            set
            {
                _annunciPrenotati = value;
                OnPropertyChanged();
            }
        }

        readonly IAnnuncioService _annuncioService;
        public AnnunciUtenteViewModel(INavService navService, AnnuncioService annuncioService) : base(navService)
        {
            _annuncioService = annuncioService;
        }


        /**
        * IMPORTANTE: Nello stato attuale, la ListView fa laggare
        * vistosamente l'app, trovare un modo di rendere più veloce
        * ed efficiente lo scroll
        */
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

            // Da modificare
            ObservableCollection<FileImageSource> percorsiImmagine = new ObservableCollection<FileImageSource>();
            percorsiImmagine.Add("tosaerba.jpg");
            percorsiImmagine.Add("tosaerba.jpg");

            IsBusy = false;
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
            // non fa nada
        }

    }
}
