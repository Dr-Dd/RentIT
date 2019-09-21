using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RentIT.Services;
using Xamarin.Forms;
using RentIT.Models.Annuncio;
using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Services.Foto;
using System;
using RentIT.Models.User;

namespace RentIT.ViewModels
{
    public class AnnunciPageViewModel : BaseViewModel<Utente>
    {
        Utente _utente;
        public Utente Utente
        {
            get { return _utente; }
            set
            {
                _utente = value;
                OnPropertyChanged();
            }
        }

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

        /*Questa proprietà serve esclusivamente a visualizzare il titolo*/
        string _titolo;
        public string Titolo
        {
            get
            { return _titolo; }
            set
            {
                _titolo = value;
                OnPropertyChanged();
            }
        }

        readonly IAnnuncioService _annuncioService;
        readonly FotoService _fotoService;
        public AnnunciPageViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
            Annunci = new ObservableCollection<Ad>();
        }
        
        public async override Task Init(Utente utente)
        {
            IsBusy = true;
            Utente = utente;
            Annunci = await _annuncioService.GetUserAds(Utente.id, false);
            await LoadImages();
            Titolo = String.Format("Annunci di {0} {1}", Utente.name, Utente.surname);
            IsBusy = false;
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

