using RentIT.Models;
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
	public partial class SearchPage : MasterDetailPage
	{
        public List<MenuEntry> MenuList { get; set; }

		public SearchPage ()
		{
			InitializeComponent ();

            MenuList = new List<MenuEntry>();

            var loginPage = new MenuEntry() { Title = "Login", Icon = "outline_persom_black_18dp.png", TypeTarget = typeof(LoginPage) };

            MenuList.Add(loginPage);

            navigationDrawerList.ItemsSource = MenuList;

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