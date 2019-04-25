using RentIT.Models;
using RentIT.Services;
using RentIT.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
                    Title = "Login",
                    Icon = "outline_person_black_18dp.png",
                    TypeTarget = typeof(LoginPage)
                });
            });
        }
    }
}