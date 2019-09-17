using RentIT.Models;
using RentIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnnunciPrenotati : ContentPage
	{

        AnnunciPrenotatiViewModel _vm
        {
            get { return BindingContext as AnnunciPrenotatiViewModel; }
        }

        public AnnunciPrenotati ()
		{
			InitializeComponent ();
		}

        async void GestioneAnnuncio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var annuncio = (Annuncio)e.Item;
            _vm.ViewGestioneAnnuncio.Execute(annuncio);

            listaAnnunciPrenotati.SelectedItem = null;
        }
    }
}