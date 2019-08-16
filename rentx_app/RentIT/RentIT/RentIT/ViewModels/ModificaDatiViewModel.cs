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

namespace RentIT.ViewModels
{
	public class ModificaDatiViewModel : BaseViewModel
	{
        readonly IUserService _userService;
        public ModificaDatiViewModel (INavService navService, UserService userService) : base(navService)
		{
            _userService = userService;
        }

        public override async Task Init()
        {

        }

        /**Comando per passare alla pagina di modifica password*/
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

        /**Comando per passare alla pagina di modifica email**/
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

        /*Comando per aggiungere o modificare la propic
         Ancora da testare perché manca utente/addImage*/
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
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                MemoryStream copy = new MemoryStream();
                stream.CopyTo(copy);
                byte[] bytes = copy.ToArray();
                string base64 = Convert.ToBase64String(bytes);

                await _userService.UploadUserImageAsync(base64);
            }
        }
    }
}