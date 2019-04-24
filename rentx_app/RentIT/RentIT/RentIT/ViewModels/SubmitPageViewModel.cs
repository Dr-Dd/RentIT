using System.Threading.Tasks;
using RentIT.Services;

namespace RentIT.ViewModels
{
    internal class SubmitPageViewModel : BaseViewModel
    {
        private INavService navService;

        public SubmitPageViewModel(INavService navService) : base(navService)
        {
        }

        public override async Task Init()
        {
        }
    }
}