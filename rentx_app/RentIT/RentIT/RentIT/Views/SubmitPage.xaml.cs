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
			InitializeComponent ();
		}

        async void OnSubmitClicked(object sender, EventArgs e)
        {
            var utente = (Utente)BindingContext;
            if (utente.isBlankfield()) blankField.IsVisible = true;
            else
            {
                await App.CredManager.SaveTaskAsync(utente);
                await Navigation.PopAsync();
            }
        }
    }
}