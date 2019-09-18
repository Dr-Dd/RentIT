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
	public partial class AnnunciAttivi : ContentPage
	{
        AnnunciUtenteViewModel _vm
        {
            get { return BindingContext as AnnunciUtenteViewModel; }
        }

        public AnnunciAttivi ()
		{
			InitializeComponent ();
		}

        async void GestioneAnnuncio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var annuncio = (Annuncio)e.Item;
            _vm.ViewGestioneAnnuncio.Execute(annuncio);

            listaAnnunci.SelectedItem = null;
        }
    }
}