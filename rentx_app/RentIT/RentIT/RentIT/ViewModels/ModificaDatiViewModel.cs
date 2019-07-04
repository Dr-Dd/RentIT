using RentIT.Services;
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
    }
}