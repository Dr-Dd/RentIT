using App.Models.Image;
using App.Services.Foto;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Models.User;
using RentIT.Services;
using RentIT.Services.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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

        ObservableCollection<OggettoImmagine> _oggettiImmagine;
        public ObservableCollection<OggettoImmagine> OggettiImmagine
        {
            get
            {
                return _oggettiImmagine;
            }
            set
            {
                _oggettiImmagine = value;
                OnPropertyChanged();
            }
        }

        readonly FotoService _fotoService;
        readonly IUserService _userService;
        public DettaglioAnnuncioViewModel(INavService navService, FotoService fotoService, UserService userService) : base(navService)
        {
            _fotoService = fotoService;
            _userService = userService;
        }

        public async override Task Init(Ad annuncio)
        {
            Annuncio = annuncio;
            //Affittuario = _userService.GetProfileAsync(annuncio.AffittuarioId);
            //ImmagineUtente = await getPropic();
            OggettiImmagine = creaOggettiImmagine(Annuncio);
        }

        public ObservableCollection<OggettoImmagine> creaOggettiImmagine(Ad annuncio)
        {
            OggettiImmagine = new ObservableCollection<OggettoImmagine>();
            foreach (FileImageSource image in Annuncio.PercorsiImmagine)
            {
                OggettiImmagine.Add(new OggettoImmagine()
                {
                    Immagine = image
                });
            }
            return OggettiImmagine;
        }

        //Metodo per prendere l'immagine profilo dell'utente dal database
        public async Task<Image> getPropic()
        {
            ImageModel foto = await _fotoService.GetUserImage(Annuncio.AffittuarioId);
            Image img = new Image();
            if (foto != null)
                img = _fotoService.fromStringToImage(foto.data);
            return img;
        }

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
                PhoneDialer.Open("6666666666666");
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
            destinatario.Add("tiziocaio@papapa.it");
            var message = new EmailMessage
            {
                To = destinatario,
            };

            await Email.ComposeAsync(message);
        }
    }
}

