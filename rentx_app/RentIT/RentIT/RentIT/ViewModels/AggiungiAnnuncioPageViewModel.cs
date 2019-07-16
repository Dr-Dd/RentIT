using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services;

namespace RentIT.ViewModels
{
    public class AggiungiAnnuncioPageViewModel : BaseViewModel
    {
        public AggiungiAnnuncioPageViewModel(INavService navService) : base(navService)
        {
        }

        public async override Task Init()
        {
        }
    }
}
