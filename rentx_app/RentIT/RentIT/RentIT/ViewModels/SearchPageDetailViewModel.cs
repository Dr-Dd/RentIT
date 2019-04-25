using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services;

namespace RentIT.ViewModels
{
    internal class SearchPageDetailViewModel : BaseViewModel<MenuEntry>
    {
        public SearchPageDetailViewModel(INavService navService) : base(navService)
        {
        }

        public override Task Init(MenuEntry item)
        {
            throw new System.NotImplementedException();
        }
    }
}