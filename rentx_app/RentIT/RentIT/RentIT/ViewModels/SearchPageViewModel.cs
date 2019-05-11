using RentIT.Models;
using RentIT.Services;
using RentIT.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        ObservableCollection<MenuEntry> _menuList;
        public ObservableCollection<MenuEntry> MenuList
        {
            get { return _menuList; }
            set
            {
                _menuList = value;
                OnPropertyChanged();
            }
        }

        public SearchPageViewModel(INavService navService): base(navService)
        {
            MenuList = new ObservableCollection<MenuEntry>();
        }

        public override async Task Init()
        {
            await LoadEntries();
        }

        async Task LoadEntries()
        {
            MenuList.Clear();

            await Task.Factory.StartNew(() =>
            {
                MenuList.Add(new MenuEntry()
                {
                    Title = "LogIn",
                    Icon = "outline_person_black_18dp.png",
                    ViewModelName = "Login"
                });

                MenuList.Add(new MenuEntry()
                {
                    Title = "TilePage",
                    Icon = "outline_person_black_18dp.png",
                    ViewModelName =  "TilePage"
                });
            });
        }

        Command _loginCommand;
        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (
                    _loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }

        async Task ExecuteLoginCommand()
        {
            if (AppSettings.AccessToken.Equals(string.Empty))
            {
                await NavService.NavigateTo<LoginPageViewModel>();
            }
            //TODO else apre la pagina utente che deve ancora essere creata
        }

        Command<string> _navigateCommand;
        public Command<string> NavigateCommand
        {
            get
            {
                return _navigateCommand
                    ?? (new Command<string>(async (vmn) => await ExecuteNavigateCommand(vmn)));
            }
        }

        /**
         * Attenzione a guardare in maniera prolungata questa funzione,
         * rischia di causare cecità e ulcere (ma vi assicuro che non c'è
         * un metodo più semplice, e no, non possiamo usare degli enum
         * visto che si usa xaml siamo costretti
         * a legarci alle stringhe, ricordatevi sempre di modificare questa funzione
         * per aggiungere pagine)
         */ 
        async Task ExecuteNavigateCommand(string viewModelName)
        {
            switch(viewModelName)
            {
                case "Login":
                    await NavService.NavigateTo<LoginPageViewModel>();
                    break;
                case "TilePage":
                    await NavService.NavigateTo<TilePageViewModel>();
                    break;
            }
        }
    }
}