
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Essentials;
using RentIT.Models.User;
using RentIT.Services.Foto;
using RentIT.Models.Immagine;

namespace RentIT.ViewModels
{
    public class DettaglioAnnuncioViewModel : BaseViewModel<Ad>
    {
        Ad _annuncio;
        public Ad Annuncio
        {
            get { return _annuncio; }
            set
            {
                _annuncio = value;
                OnPropertyChanged();
            }
        }

        Utente _affittuario;
        public Utente Affittuario
        {
            get { return _affittuario; }
            set
            {
                _affittuario = value;
                OnPropertyChanged();
            }
        }

        Image _immagineUtente;
        public Image ImmagineUtente
        {
            get { return _immagineUtente; }
            set
            {
                _immagineUtente = value;
                OnPropertyChanged();
            }
        }

        List<Image> _immagini;
        public List<Image> Immagini
        {
            get
            {
                return _immagini;
            }
            set
            {
                _immagini = value;
                OnPropertyChanged();
            }
        }

        /*Questa proprietà serve esclusivamente a visualizzare il nome e cognome vicini*/
        string _nomeAffittuario;
        public string NomeAffittuario
        {
            get
            {    return _nomeAffittuario;   }
            set
            {
                _nomeAffittuario = value;
                OnPropertyChanged();
            }
        }


        readonly FotoService _fotoService;
        readonly IUserService _userService;
        public DettaglioAnnuncioViewModel(INavService navService, FotoService fotoService, UserService userService) : base(navService)
        {
            _fotoService = fotoService;
            _userService = userService;
            Immagini = new List<Image>();
        }

        public async override Task Init(Ad annuncio)
        {
            IsBusy = true;
            Annuncio = annuncio;
            Immagini = await LoadAdImages();
            Affittuario = await _userService.GetUserByIdAsync(Annuncio.AffittuarioId);
            ImmagineUtente = await getPropic();
            NomeAffittuario = String.Format("{0} {1}", Affittuario.name, Affittuario.surname);
            IsBusy = false;
        }

        public async Task<List<Image>> LoadAdImages()
        {
            var listaImageModel = await _fotoService.GetAdImagesAsync(Annuncio.id);
            var listaImmaginiAnnuncio = new List<Image>();

            foreach (var imgModel in listaImageModel)
            {
                listaImmaginiAnnuncio.Add(_fotoService.fromStringToImage(imgModel.data));
            }

            return listaImmaginiAnnuncio;
        }

        public async Task<Image> getPropic()
        {
            ImageModel imgModel = await _fotoService.GetUserImage(Affittuario.id);
            if (imgModel.data != null)
            {
                Image img = _fotoService.fromStringToImage(imgModel.data);
                return img;
            }
            else
            {
                Image img = new Image();
                img.Source = "userImg.png";
                return img;
            }
        }
        
        /*
         * Cliccando sul nome utente si apre la sua scheda
         */
        Command _infoUtenteCommand;
        public Command InfoUtenteCommand
        {
            get
            {
                return _infoUtenteCommand
                    ?? (_infoUtenteCommand = new Command(async () => await ExecuteInfoUtenteCommand()));
            }
        }


        async Task ExecuteInfoUtenteCommand()
        {
            await NavService.NavigateTo<InfoUtenteViewModel, Utente>(Affittuario);
        }

        /*
         * Comando associato al bottone "chiama"
         */
        Command _callCommand;
        public Command CallCommand
        {
            get
            {
                return _callCommand
                    ?? (_callCommand = new Command(async () => await ExecuteCallCommand()));
            }
        }

        async Task ExecuteCallCommand()
        {
            try
            {
                //Da rimuovere le virgolette
                PhoneDialer.Open(Affittuario.numeroTel);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.  
            }
            catch (Exception ex)
            {
                // Other error has occurred.  
            }
        }

        /*
         * Comando associato al bottone "manda email"
         */
        Command _emailCommand;
        public Command EmailCommand
        
        {
            get
            {
                return _emailCommand
                    ?? (_emailCommand = new Command(async () => await ExecuteEmailCommand()));
                
            }
        }

        async Task ExecuteEmailCommand()
        {
            List<String> destinatario = new List<string>();
            destinatario.Add(Affittuario.email);
            var message = new EmailMessage
            {
                To = destinatario,
            };

            await Email.ComposeAsync(message);
        }
    }
}
