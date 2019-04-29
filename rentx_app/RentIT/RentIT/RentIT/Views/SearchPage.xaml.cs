using RentIT.Models;
using RentIT.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : MasterDetailPage
	{
        SearchPageViewModel _vm
        {
            get { return BindingContext as SearchPageViewModel; }
        }

		public SearchPage ()
		{
			InitializeComponent ();

            var loginPage = new MenuEntry()
            {
                Title = "Login",
                Icon = "outline_person_black_18dp.png",
                TypeTarget = typeof(LoginPage)
            };

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(SearchPageDetail)));

            this.BindingContext = new
            {
                Header = "",
                Image = "",
                Footer = "Benvenuto in RentIT"
            };
		}

        private void OnMenuItemSelected (object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuEntry)e.SelectedItem;
            Type page = item.TypeTarget;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
	}
}