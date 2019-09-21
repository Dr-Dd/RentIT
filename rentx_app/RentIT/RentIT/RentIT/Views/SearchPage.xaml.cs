using RentIT.Models;
using RentIT.ViewModels;
using RentIT.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
    /* NB********************************************
     * Per motivi a me ancora oscuri, Microsoft (anche conosciuta come 'Miguel De Icaza')
     * ha pensato che una MasterDetailPage, composta da 2 view, abbia un solo 
     * bindingContext, relativo alla Master Page. Poco male! La cosa ha molto senso
     * visto che comunque a livello pratico si utilizza una sola pagina. Peccato che ciò
     * NON SIA SCRITTO SU ALCUNA FORMA DI DOCUMENTAZIONE MAI RILASCIATA PER QUESTO 
     * FRAMEWORK. Detto ciò, tutto ciò che ha a che fare con MVVM va gestito dalla sola 
     * SearchPageViewModel.
     *
     * ~drd
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : MasterDetailPage
    {

        /**
         * NB: Creaimo questa property SOLO (E SOLO) nel momento in cui
         * E' NECESSARIO UTILIZZARE PARAMETRI O ELEMENTI CONTENUTI NEL VIEWMODEL!!!
         * In caso contrario NON serve (inserirlo non è un errore, ma pulisce sicuramente
         * il codice)
         */
        SearchPageViewModel _vm
        {
            get { return BindingContext as SearchPageViewModel; }
        }

        public SearchPage()
        {
            InitializeComponent();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(SearchPageDetail)));
        }

        async void IMieiAnnunciPage_Tapped(object sender, EventArgs e)    
        {
            _vm.AnnunciUtenteCommand.Execute(null);
        }

        async void Logout_Tapped(object sender, EventArgs e)    
        {
            _vm.LogOutCommand.Execute(null);
        }

        async void Login_Tapped(object sender, EventArgs e)    
        {
            _vm.LoginCommand.Execute(null);
        }
        /* L'override di questo metodo è necessario poichè non è possibile
         * avviare attraverso qualche comando la login page, prima pagina
         * del programma (ecco perché ci serve '_vm')*/
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
                await _vm.Init();
        }

        private void TextCell_Tapped(object sender, EventArgs e)
        {

        }
    }
}