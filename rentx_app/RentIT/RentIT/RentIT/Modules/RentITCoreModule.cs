using Ninject.Modules;
using RentIT.Services.Authentication;
using RentIT.Services.Request;
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
            Bind<TilePageViewModel>().ToSelf();

            var requestService = new RequestService();
            var authService = new AuthenticationService(requestService);

            Bind<IRequestService>().
                ToMethod(x => requestService)
                .InSingletonScope();

            Bind<IAuthenticationService>()
                .ToMethod(x => authService)
                .InSingletonScope();

        }
    }
}
