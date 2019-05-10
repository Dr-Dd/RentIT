using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.ViewModels;
using RentIT.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TilePage : ContentPage
	{
        /**
         * Ancora una volta, questo non andrebbe usato, la presenza di un hamburger menu
         * però costringe alla sua implementazione (altrimenti la pagina non può venire "avviata"
         */
        TilePageViewModel _vm
        {
            get { return BindingContext as TilePageViewModel;  }
        }

        public TilePage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_vm != null)
                await _vm.Init();

        }
    }
}