using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentIT.Services;
using RentIT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AggiungiAnnuncioPage : ContentPage
	{

        AggiungiAnnuncioViewModel _vm
        {
            get { return _vm as AggiungiAnnuncioViewModel; }
        }

        public AggiungiAnnuncioPage()
        {
            InitializeComponent();
        }
    }
}