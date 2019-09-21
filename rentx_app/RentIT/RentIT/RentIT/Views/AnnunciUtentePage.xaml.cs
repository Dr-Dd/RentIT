using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.ViewModels;
using RentIT.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnunciUtentePage : TabbedPage
    {

        AnnunciUtenteViewModel _vm
        {
            get { return BindingContext as AnnunciUtenteViewModel; }
        }

        public AnnunciUtentePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
                await _vm.Init();
        }

        async void GestioneAnnuncio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var annuncio = (Ad)e.Item;
            _vm.ViewGestioneAnnuncio.Execute(annuncio);

            //listaAnnunci.SelectedItem = null;
        }
    }
}