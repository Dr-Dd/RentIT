using RentIT.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.ViewModels
{
    class ModificaPasswordViewModel : BaseViewModel
    {
        public ModificaPasswordViewModel(INavService navService) : base(navService)
        {
        }

        public async override Task Init()
        {
        }
    }
}
