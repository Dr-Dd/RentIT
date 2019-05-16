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
    public partial class SearchPageDetail : ContentPage
    {
        /**
         * Per qualche motivo, settare il Binding Context della detail
         * page fa crashare l'app, da indagare
         */
        //SearchPageDetailViewModel _vm
        //{
        //    get { return _vm as SearchPageDetailViewModel;  }
        //}

        // TODO: ANTI-PATTERN, va spostato nel viewModel
        /**Lista di città rudimentale per provare un suggerimento dinamico**/
        private List<string> cities = new List<string>()
        {
            "Roma", "Napoli", "Milano", "Genova", "Bologna", "Palermo", "Campobasso", "Venezia", "Torino", "Firenze"
        };

		public SearchPageDetail ()
		{
			InitializeComponent ();
		}

        // TODO: ANTI-PATTERN, va spostato nel viewModel
        /**Fa si che vengano suggerite delle città nel momento in cui si inizia digitare sulla tastiera (Momentanei)**/
        private void CitySearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = citySearchBar.Text;

            var suggestions = cities.Where(c => c.ToLower().Contains(keyword.ToLower()));

            suggestionListView.ItemsSource = suggestions;
        }

        // TODO: ANTI-PATTERN, va spostato nel viewModel
        /**Una volta selezionata la città la imposta come testo della barra di ricerca (Momentanei)**/
        private void SuggestionListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            citySearchBar.Text = e.Item.ToString();
        }

        /**
         * Vedi commento in testa alla pagina
         */
        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (_vm != null)
        //        await _vm.Init();
        //}

    }
}