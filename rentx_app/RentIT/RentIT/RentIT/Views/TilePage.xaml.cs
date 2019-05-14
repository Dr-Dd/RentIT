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
        TilePageViewModel _vm
        {
            get { return BindingContext as TilePageViewModel; }
        }

        public TilePage ()
		{
			InitializeComponent ();
		}

        async void Annuncio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var annuncio = (Annuncio)e.Item;
            _vm.ViewAnnuncio.Execute(annuncio);

            listaAnnunci.SelectedItem = null;
        }
    }
}