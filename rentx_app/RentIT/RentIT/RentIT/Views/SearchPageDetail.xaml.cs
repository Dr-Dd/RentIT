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

        /**Lista di città rudimentale per provare un suggerimento dinamico**/
        private List<string> cities = new List<string>()
        {
            "Roma", "Napoli", "Milano", "Genova", "Bologna", "Palermo", "Campobasso", "Venezia", "Torino", "Firenze"
        };

		public SearchPageDetail ()
		{
			InitializeComponent ();
		}

        /**Fa si che vengano suggerite delle città nel momento in cui si inizia digitare sulla tastiera (Momentanei)**/
        private void CitySearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = citySearchBar.Text;

            var suggestions = cities.Where(c => c.ToLower().Contains(keyword.ToLower()));

            suggestionListView.ItemsSource = suggestions;
        }

        /**Una volta selezionata la città la imposta come testo della barra di ricerca (Momentanei)**/
        private void SuggestionListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            citySearchBar.Text = e.Item.ToString();
        }
    }
}