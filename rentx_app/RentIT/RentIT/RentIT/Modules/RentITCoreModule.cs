using Ninject.Modules;
using RentIT.Services.Authentication;
using RentIT.Services.Request;
using RentIT.Services.User;
using RentIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentIT.Modules
{
    public class RentITCoreModule : NinjectModule
    {
        public override void Load()
        {
            // Carico i ViewModels
            Bind<LoginPageViewModel>().ToSelf();
            Bind<SearchPageViewModel>().ToSelf();
            Bind<SearchPageDetailViewModel>().ToSelf();
            Bind<SubmitPageViewModel>().ToSelf();
            Bind<AnnunciPageViewModel>().ToSelf();
            Bind<UtentePageViewModel>().ToSelf();
            Bind<DettaglioAnnuncioViewModel>().ToSelf();
            Bind<AggiungiAnnuncioViewModel>().ToSelf();

            // E' importante fare il binding di tutti i servizi che
            // non servono in quantità molteplice nelle injection

            // Qui crei le istanze
            var requestService = new RequestService();
            var authService = new AuthenticationService(requestService);
            var userService = new UserService(requestService);

            // Qui gli fai il binding come singoletto
            Bind<IRequestService>().
                ToMethod(x => requestService)
                .InSingletonScope();

            // ***
            Bind<IAuthenticationService>()
                .ToMethod(x => authService)
                .InSingletonScope();

            // ***
            Bind<IUserService>()
                .ToMethod(x => userService)
                .InSingletonScope();

        }
    }
}
