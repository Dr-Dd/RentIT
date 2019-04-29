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
    public partial class LoginPage : ContentPage
    {
        /**
         * NB: Creaimo questa property SOLO (E SOLO) nel momento in cui
         * E' NECESSARIO UTILIZZARE PARAMETRI O ELEMENTI CONTENUTI NEL VIEWMODEL!!!
         * In caso contrario NON serve (inserirlo non è un errore, ma pulisce sicuramente
         * il codice)
         */
        LoginPageViewModel _vm
        {
            get { return BindingContext as LoginPageViewModel; }
        }

        public LoginPage()
        {
            InitializeComponent();
        }

        /* L'override di questo metodo è necessario poichè non è possibile
         * avviare attraverso qualche comando la login page, prima pagina
         * del programma (ecco perché ci serve '_vm')*/
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
                await _vm.Init();
        }
    } 
 }
    
