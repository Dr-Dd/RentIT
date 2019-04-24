using RentIT.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.ViewModels
{
    /**
     * SuperClasse ViewModel ereditata da tutti gli altri ViewModel.
     * Notare l'overloading di classe, che può avere o meno un parametro */
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected INavService NavService { get; private set; }

        public abstract Task Init();

        protected BaseViewModel(INavService navService)
        {
            NavService = navService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel(INavService navService) : base(navService)
        {

        }

        public override async Task Init()
        {
            await Init(default(TParameter));
        }

        public abstract Task Init(TParameter parameter);
    }
}
