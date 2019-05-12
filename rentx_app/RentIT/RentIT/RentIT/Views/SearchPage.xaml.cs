﻿using RentIT.Models;
using RentIT.ViewModels;
using RentIT.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : MasterDetailPage
    {

        /**
         * NB: Creaimo questa property SOLO (E SOLO) nel momento in cui
         * E' NECESSARIO UTILIZZARE PARAMETRI O ELEMENTI CONTENUTI NEL VIEWMODEL!!!
         * In caso contrario NON serve (inserirlo non è un errore, ma pulisce sicuramente
         * il codice)
         */
        SearchPageViewModel _vm
        {
            get { return BindingContext as SearchPageViewModel; }
        }

        public SearchPage()
        {
            InitializeComponent();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(SearchPageDetail)));

        }

        private async void Views_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (MenuEntry)e.Item;
            EnumMenuEntry vn  = item.ViewName;

            _vm.NavigateCommand.Execute(vn);
            navigationDrawerList.SelectedItem = null;
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