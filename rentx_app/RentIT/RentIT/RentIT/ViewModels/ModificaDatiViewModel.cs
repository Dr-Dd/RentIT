using RentIT.Services;
using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
	public class ModificaDatiViewModel : BaseViewModel
	{
		public ModificaDatiViewModel (INavService navService) : base(navService)
		{
			
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
    }
}