using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentIT.Models;
using RentIT.Services;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class AnnunciAttiviViewModel : BaseViewModel
    {
       

        public AnnunciAttiviViewModel(INavService navService) : base(navService)
        {
            
        }

        public override async Task Init()
        {

        }

    }
}
