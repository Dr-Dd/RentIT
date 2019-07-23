using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.ViewModels
{
    class ModificaEmailViewModel : BaseViewModel
    {
        public ModificaEmailViewModel(INavService navService) : base(navService)
        {
        }

        public async override Task Init()
        {
        }
    }
}
