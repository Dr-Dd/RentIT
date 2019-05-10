using RentIT.Models;
using RentIT.ViewModels;
using RentIT.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
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
            /**
            var loginPage = new MenuEntry()
            {
                Title = "Login",
                Icon = "outline_person_black_18dp.png",
                TypeTarget = typeof(LoginPage)
            };

            var tilePage = new MenuEntry()
            {
                Title = "TilePage",
                Icon = "outline_person_black_18dp.png",
                TypeTarget = typeof(TilePage)
            };
            **/
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(SearchPageDetail)));

        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuEntry)e.SelectedItem;
            Type page = item.TypeTarget;

            await NavService.NavigateTo<page>();
            IsPresented = true;
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
    }
}