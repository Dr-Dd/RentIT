using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using RentIT.ViewModels;

namespace RentIT.Services
{
    /**
     * Implementazione di interfaccia di navigazione, presa dalla documentazione
     * Xamarin (Mastering Xamarin Forms), per approfondimenti riferirsi al 
     * testo */
    public interface INavService
    {
        bool CanGoBack { get; }
        Task GoBack();
        Task NavigateTo<TVM>()
            where TVM : BaseViewModel;
        Task NavigateTo<TVM, TParameter>(TParameter parameter)
            where TVM : BaseViewModel;
        Task RemoveLastView();
        Task ClearBackStack();
        Task NavigateToUri(Uri uri);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
