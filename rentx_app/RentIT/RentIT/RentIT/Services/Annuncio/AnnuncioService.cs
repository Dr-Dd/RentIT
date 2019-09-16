using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentIT;
using RentIT.Models.Annuncio;
using RentIT.Models.User;
using RentIT.Services.Request;

namespace App.Services.Annuncio
{
    public class AnnuncioService : IAnnuncioService
    {

        private readonly IRequestService requestService;


        public AnnuncioService(IRequestService requestService)
        {
            this.requestService = requestService;
        }


        public async Task<Response> AddAnnuncioAsync(Ad a)
        {
            var builder = new UriBuilder(Constants.newAnnuncioEndpoint());
            var uri = builder.ToString();
            
            Response resp = await requestService.PostAsync<Ad, Response>(uri, a, AppSettings.AccessToken);

            return resp;

        }

        //mi serve l'id dell'annuncio (che ho anche aggiunto nel model)
        //nel db bisogna poi controllare che quell'id sia effettivamente dell'utente con questo token(
        //altrimenti chiunque puo cancellare un qualsiasi annuncio)
        public async Task<Response> DeleteAdAsync(string idAd)
        {
            var builder = new UriBuilder(Constants.eliminaAnnuncioEndpoint());
            builder.Path = "/" + idAd;
            var uri = builder.ToString();

            Response resp = await requestService.DeleteAsync<Response>(uri, AppSettings.AccessToken);

            return resp;
        }
        

        //anche qui ho bisogno dell'id dell'Annuncio da modificare , anche se stavolta posso prenderlo da dentro l'annuncio,
        //in realtà però dipende da come gestiamo il fetch dal db , in ogni caso non facciamo vedere l'id all'utente ,ma nel caso 
        //lo prendessimo dal db allora posso usarlo , in caso contrario me lo dovete passare , io qui presumo che ce l'ho nell'oggetto Ad(Annuncio)
        public async Task<Response> ModifyAdDataAsync(Ad a)
        {
            var builder = new UriBuilder(Constants.modificaAnnuncioEndpoint());
            if (string.IsNullOrWhiteSpace(a.Id))
            {
                var errResp = new Response {
                    hasSucceded = false,
                    responseMessage = " errore id annuncio"
                };

                return errResp;
            }

            builder.Path = "/" + a.Id;
            var uri = builder.ToString();

            Response resp = await requestService.PutAsync<Ad, Response>(uri, a, AppSettings.AccessToken);

            return resp;
        }
    }
}
