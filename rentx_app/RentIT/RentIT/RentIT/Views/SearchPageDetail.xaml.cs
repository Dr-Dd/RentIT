using RentIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RentIT.Services;

namespace RentIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPageDetail : ContentPage
    {
		public SearchPageDetail ()
		{
			InitializeComponent ();
		}
    }
}