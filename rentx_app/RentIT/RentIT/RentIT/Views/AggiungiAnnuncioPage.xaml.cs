using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentIT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AggiungiAnnuncioPage : ContentPage
	{
        AggiungiAnnuncioPageViewModel _vm
        {
            get { return _vm as AggiungiAnnuncioPageViewModel; }
        }

        public AggiungiAnnuncioPage()
        {
            InitializeComponent();
        }

    }
}