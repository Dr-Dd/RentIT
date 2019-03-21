using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LoginDB.Models;
using Newtonsoft.Json;

namespace LoginDB.Data
{
    public class RestService: IRestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            
        }
       
        public async Task SaveUserAsync(Utente user)
        {
            // RestUrl = http://5.249.151.26:5000/
            var uri = new Uri(string.Format(Constants.RestUrl, "registrazione"));
           

            try
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
               
                response = await client.PostAsync(uri, content);
                

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("funziona");
                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            
       
        }

        public async Task DeleteUserAsync(int id)
        {
            // RestUrl = http://5.249.151.26:5000/
            var uri = new Uri(string.Format(Constants.RestUrl, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("User successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        public async Task<String> AreCredentialsValid(Credentials cr)
        {
            // RestUrl = http://5.249.151.26:5000/
            var uri = new Uri(string.Format(Constants.RestUrl,"login"));
            String result= null;

           try
            {
                var json = JsonConvert.SerializeObject(cr);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    
                }
                else result = "utente non esistente";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                result = "errore";
            }

            return result;
        }
        //create metod to get async some tiles 

    }
}

