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

        /*Comando per passare alla pagina di modifica password*/
        Command _modificaPasswordCommand;
        public Command ModificaPasswordCommand
        {
            get
            {
                return _modificaPasswordCommand
                    ?? (_modificaPasswordCommand = new Command(async () => await ExecuteModificaPasswordCommand()));
            }
        }

        async Task ExecuteModificaPasswordCommand()
        {
            await NavService.NavigateTo<ModificaPasswordViewModel>();
        }

        /*Comando per passare alla pagina di modifica email*/
        Command _modificaEmailCommand;
        public Command ModificaEmailCommand
        {
            get
            {
                return _modificaEmailCommand
                    ?? (_modificaEmailCommand = new Command(async () => await ExecuteModificaEmailCommand()));
            }
        }

        async Task ExecuteModificaEmailCommand()
        {
            await NavService.NavigateTo<ModificaEmailViewModel>();
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