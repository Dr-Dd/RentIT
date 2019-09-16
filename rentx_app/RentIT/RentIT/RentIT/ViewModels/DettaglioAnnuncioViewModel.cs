using App.Models.Image;
using App.Services.Foto;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    /**
     * Classe segnaposto, probabilmente in futuro ci sarà bisogno 
     * di implementare l'aggiunta di un oggetto tramite API
     */
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

        readonly FotoService _fotoService;
        public DettaglioAnnuncioViewModel(INavService navService, FotoService fotoService) : base(navService)
        {
            _fotoService = fotoService;
        }

        public async override Task Init(Ad annuncio)
        {
            Annuncio = annuncio;
            //ImmagineUtente = await getPropic();
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

    }
}
