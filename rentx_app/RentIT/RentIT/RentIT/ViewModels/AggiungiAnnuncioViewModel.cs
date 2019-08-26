using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using App.Services.Foto;
using RentIT.Models;
using RentIT.Services;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class AggiungiAnnuncioViewModel : BaseViewModel
    {
        Image _immagine;
        public Image Immagine
        {
            get { return _immagine ?? (new Image()); }
            set
            {
                _immagine = value;
                OnPropertyChanged();
            }
        }

        readonly FotoService _fotoService;
        public AggiungiAnnuncioViewModel(INavService navService, FotoService fotoService) : base(navService)
        {
            _fotoService = fotoService;
        }

        public async override Task Init()
        {
        }

        //Comando per aggiungere o modificare la propic
        Command _aggiungiFotoCommand;
        public Command AggiungiFotoCommand
        {
            get
            {
                return _aggiungiFotoCommand
                    ?? (_aggiungiFotoCommand = new Command(async () => await ExecuteAggiungiFotoCommand()));
            }
        }

        async Task ExecuteAggiungiFotoCommand()
        {
            //Caricare un'immagine dalla galleria
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                //se esiste, si salva nel db associato all'annuncio
                string base64 = _fotoService.fromStreamToString(stream);

                //questa riga serve solo a visualizzare l'immagine in attesa del collegamento al db
                Immagine = _fotoService.fromStringToImage(base64);

                //tipo _itemService.UploadItemImage(base64)
            }
        }
    }
}
