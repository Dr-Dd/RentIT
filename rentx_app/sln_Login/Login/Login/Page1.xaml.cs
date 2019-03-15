using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
        public Page1()
        {
            InitializeComponent();
        }
        public Page1(String nom,String cgn) : this()
		{
            welcome.Text = "benvenuto " + nom +" "+cgn;
		}

       async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            
        }
    }
}