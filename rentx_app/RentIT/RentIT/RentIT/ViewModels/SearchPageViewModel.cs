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
        ObservableCollection<MenuEntry> _menuList =
            new ObservableCollection<MenuEntry>
            {
                new MenuEntry()
                {
                    Title = "LogIn",
                    Icon = "outline_person_black_18dp.png",
                    ViewName = EnumMenuEntry.loginPage
                },
                new MenuEntry()
                {
                    Title = "TilePage",
                    Icon = "outline_person_black_18dp.png",
                    ViewName =  EnumMenuEntry.tilePage
                }
            };
        public ObservableCollection<MenuEntry> MenuList
        {
            get { return _menuList; }
        }

        public SearchPageViewModel(INavService navService) : base(navService)
        {
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

        Command<EnumMenuEntry> _navigateCommand;
        public Command<EnumMenuEntry> NavigateCommand
        {
            get
            {
                return _navigateCommand
                    ?? (new Command<EnumMenuEntry>(async (vn) => await ExecuteNavigateCommand(vn)));
            }
        }

        /**
         * L'ho migliorata poco poco...mi sono reso conto di aver detto
         * una stronzata nella mia stanchezza. Ancora una volta, però,
         * risulta essere una funzione molto brutta, l'ideale sarebbe
         * istanziare un metodo generico che accetta tipi definiti a runtime
         */
        async Task ExecuteNavigateCommand(EnumMenuEntry viewName)
        {
            switch (viewName)
            {
                case EnumMenuEntry.loginPage:
                    await NavService.NavigateTo<LoginPageViewModel>();
                    break;
                case EnumMenuEntry.tilePage:
                    await NavService.NavigateTo<TilePageViewModel>();
                    break;
            }
        }

        public override async Task Init()
        {
        }
    }
}