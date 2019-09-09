using RentIT.Models;
using RentIT.Services;
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
	public partial class SubmitPage : ContentPage
	{
		public SubmitPage ()
		{
            InitializeComponent();
		}

        /*
        public void SamePassword(object sender, TextChangedEventArgs e)
        {
            string password_string = password.Text.ToString();
            if (password.Text == confermaPassword.Text && !String.IsNullOrWhiteSpace(password_string))
            {
                bottoneIscrizione.IsVisible = true;
                bottoneIscrizione.IsEnabled = true;
            }
            else
            {
                bottoneIscrizione.IsVisible = true;
                bottoneIscrizione.IsEnabled = false;
            }
        }
        */

    }
}