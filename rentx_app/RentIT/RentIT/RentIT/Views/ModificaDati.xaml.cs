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
	public partial class ModificaDati : ContentPage
	{
		public ModificaDati ()
		{
			InitializeComponent ();
		}

        public void SamePassword (object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(password.Text) && String.IsNullOrEmpty(confermaPassword.Text))
            {
                modifica.IsEnabled = true;
                errore.IsVisible = false;
            }
            else if (password.Text != confermaPassword.Text)
            {
                modifica.IsEnabled = false;
                errore.IsVisible = true;
            }
            else
            {
                modifica.IsEnabled = true;
                errore.IsVisible = false;
            }
        }

    }
}