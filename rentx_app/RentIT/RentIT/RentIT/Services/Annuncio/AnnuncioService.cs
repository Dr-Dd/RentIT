using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentIT;
using RentIT.Models.Annuncio;
using RentIT.Models.User;
using RentIT.Services.Request;
using RentIT.DBmessages;
using RentIT.Models;
using System.Collections.ObjectModel;

namespace RentIT.Services.Annuncio
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
        public async Task<Response> DeleteAdAsync(long idAd)
        {
            
            var baseUri = Constants.eliminaAnnuncioEndpoint();
            var builder = new UriBuilder(String.Concat(baseUri, "/", idAd));
            var uri = builder.ToString();

            Response resp = await requestService.DeleteAsync<Response>(uri, AppSettings.AccessToken);

            return resp;
        }


        //anche qui ho bisogno dell'id dell'Annuncio da modificare , anche se stavolta posso prenderlo da dentro l'annuncio,
        //in realtà però dipende da come gestiamo il fetch dal db , in ogni caso non facciamo vedere l'id all'utente ,ma nel caso 
        //lo prendessimo dal db allora posso usarlo , in caso contrario me lo dovete passare , io qui presumo che ce l'ho nell'oggetto Ad(Annuncio)
        public async Task<Response> ModifyAdDataAsync(Ad a)
        {
           
            if (a.id==0)  //da capire se questo è giusto
            {
                var errResp = new Response {
                    hasSucceded = false,
                    responseMessage = " errore id annuncio"
                };

                return errResp;
            }

            var baseUri = Constants.modificaAnnuncioEndpoint();
            var builder = new UriBuilder(String.Concat(baseUri, "/", a.id));
            var uri = builder.ToString();

            Response resp = await requestService.PutAsync<Ad, Response>(uri, a, AppSettings.AccessToken);

            return resp;
        }


        public async Task<ObservableCollection<Ad>> GetLastAds(string _citta,string _oggetto)
        {
            var builder = new UriBuilder(Constants.UltimiAnnunciEndpoint());
            var uri = builder.ToString();

            var sq = new SearchQuery
            {
                citta = _citta,
                oggetto = _oggetto
            };

            return await requestService.PostAsync<SearchQuery,ObservableCollection<Ad>>(uri,sq, AppSettings.AccessToken);

        }

        public async Task<ObservableCollection<Ad>> GetUserAds(long UserId,bool booked)
        {
            
            var baseUri = Constants.AnnunciPerUserEndpoint();
            var builder = new UriBuilder(String.Concat(baseUri, "/", UserId));
            var uri = builder.ToString();

            var rq = new DBmessages.Request
            {
                b = booked,
            };

            return await requestService.PostAsync<DBmessages.Request, ObservableCollection<Ad>>(uri, rq, AppSettings.AccessToken);

         }


        //annunci non prenotati dell'user 
        public Task<ObservableCollection<Ad>> GetMyNotBookedAds()
        {
            return GetUserAds(AppSettings.UserId,false);
        }

        //annunci prenotati dell'user
        public Task<ObservableCollection<Ad>> GetMyBookedAds()
        {
            return GetUserAds(AppSettings.UserId,true);
        }

        public async Task<Ad> GetSingoloAnnuncio(long idAnn)
        {
            var baseUri = Constants.AnnuncioPerId();
            var builder = new UriBuilder(String.Concat(baseUri, "/", idAnn));
            var uri = builder.ToString();

            return await requestService.GetAsync<Ad>(uri, AppSettings.AccessToken);
        }

        public async Task prenotaAd(long idAnn)
        {
            var baseUri = Constants.PrenotaAnnuncio();
            var builder = new UriBuilder(String.Concat(baseUri, "/", idAnn));
            var uri = builder.ToString();

            await requestService.GetAsync<bool>(uri, AppSettings.AccessToken);
        }

        public async Task liberaAd(long idAnn)
        {
            var baseUri = Constants.LiberaAnnuncio();
            var builder = new UriBuilder(String.Concat(baseUri, "/", idAnn));
            var uri = builder.ToString();

            await requestService.GetAsync<bool>(uri, AppSettings.AccessToken);
        }

        public async Task<bool> isAdBooked(long idAnn)
        {
            var baseUri = Constants.isAdBooked();
            var builder = new UriBuilder(String.Concat(baseUri, "/", idAnn));
            var uri = builder.ToString();

            return await requestService.GetAsync<bool>(uri, AppSettings.AccessToken);
        }


    }
}
