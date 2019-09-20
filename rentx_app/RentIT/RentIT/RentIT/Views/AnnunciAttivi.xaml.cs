using RentIT.Models;
using RentIT.Models.Annuncio;
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
        AnnunciAttiviViewModel _vm
        {
            get { return BindingContext as AnnunciAttiviViewModel; }
        }

        public AnnunciAttivi()
        {
            InitializeComponent();
        }

        async void GestioneAnnuncio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var annuncio = (Ad)e.Item;
            _vm.ViewGestioneAnnuncio.Execute(annuncio);

            listaAnnunci.SelectedItem = null;
        }
    }
}