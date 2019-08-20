using RentIT.Services;
using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using RentIT.Services.User;
using App.Services.Foto;

namespace RentIT.ViewModels
{
	public class ModificaDatiViewModel : BaseViewModel
	{
        readonly IUserService _userService;
        readonly FotoService _fotoService;
        public ModificaDatiViewModel (INavService navService, UserService userService, FotoService fotoService) : base(navService)
		{
            _userService = userService;
            _fotoService = fotoService;
        }

        public override async Task Init()
        {

        }

        /*Comando per eliminare l'account*/
        Command _eliminaCommand;
        public Command EliminaCommand
        {
            get
            {
                return _eliminaCommand
                    ?? (_eliminaCommand = new Command(async () => await ExecuteEliminaCommand()));
            }
        }

        async Task ExecuteEliminaCommand()
        {
            IsBusy = true;
            var response = await _userService.DeleteAccount();
            if(response.hasSucceded)
            {
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }
            IsBusy = false;
        }

        //Comando per aggiungere o modificare la propic
        Command _modificaFotoCommand;
        public Command ModificaFotoCommand
        {
            get
            {
                return _modificaFotoCommand
                    ?? (_modificaFotoCommand = new Command(async () => await ExecuteModificaFotoCommand()));
            }
        }

        async Task ExecuteModificaFotoCommand()
        {
            //Caricare un'immagine dalla galleria
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                //se esiste, si salva nel db associato all'utente
                string base64 = _fotoService.fromStreamToString(stream);
                await _userService.UploadUserImageAsync(base64);
            }
        }
    }
}