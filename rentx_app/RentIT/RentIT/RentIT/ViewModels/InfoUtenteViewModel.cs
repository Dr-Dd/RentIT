using RentIT.Models.Annuncio;
using RentIT.Models.Immagine;
using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.Annuncio;
using RentIT.Services.Foto;
using RentIT.Services.User;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class InfoUtenteViewModel : BaseViewModel<Utente>
    {

        Image _immagine;
        public Image Immagine
        {
            get
            {
                return _immagine;
            }
            set
            {
                _immagine = value;
                OnPropertyChanged();
            }
        }


        Utente _utente;
        public Utente Utente
        {
            get
            {
                return _utente;
            }
            set
            {
                _utente = value;
                OnPropertyChanged();
            }
        }

        /*Questa proprietà serve esclusivamente a visualizzare il nome e cognome vicini*/
        string _nomeUtente;
        public string NomeUtente
        {
            get
            { return _nomeUtente; }
            set
            {
                _nomeUtente = value;
                OnPropertyChanged();
            }
        }

        readonly UserService _utenteService;
        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;

        public InfoUtenteViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService, UserService utenteService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
            _utenteService = utenteService;

        }

        public async override Task Init(Utente utente)
        {
            Utente = utente;
            Immagine = await LoadUserPic(utente.id);
            NomeUtente = String.Format("{0} {1}", Utente.name, Utente.surname);

        }

        public async Task<Image> LoadUserPic(long id)
        {
            ImageModel imgModel = await _fotoService.GetUserImage(id);
            if(imgModel.data != null)
            {
                Image img = _fotoService.fromStringToImage(imgModel.data);
                return img;
            }
            else
            {
                Image img = new Image();
                img.Source = "meme.png";
                return img;
            }
        }

        Command _vediAnnunciUtenteCommand;
        public Command VediAnnunciUtenteCommand
        {
            get
            {
                return _vediAnnunciUtenteCommand
                    ?? (_vediAnnunciUtenteCommand = new Command(async () => await ExecuteVediAnnunciUtenteCommand()));
            }
        }

        async Task ExecuteVediAnnunciUtenteCommand()
        {
            ObservableCollection<Ad> asd = await _annuncioService.GetUserAds(Utente.id, false);
            await NavService.NavigateTo<AnnunciPageViewModel, Utente>(Utente);
        }
    }
}
