using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginDB.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TilePage : ContentPage
	{
		public TilePage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //listView.ItemsSource = await App.TodoManager.GetTasksAsync();
        }
    }
}