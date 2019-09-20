
using RentIT.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json.Serialization;

namespace RentIT.Services.Request
{
    public class RequestService : IRequestService
    {
        readonly JsonSerializerSettings serializerSettings;

        public RequestService()
        {
            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token="")
        {
            HttpClient httpClient = CreateHttpClient(token);
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);

            var serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings);

            return result;
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data,string token="")
        {
            return PostAsync<TResult, TResult>(uri, data,token);
        }


        public async Task<TResult> PostAsync<TRequest,TResult>(string uri, TRequest data, string token="")
        {
            HttpClient httpClient = CreateHttpClient(token);
            var serialized = JsonConvert.SerializeObject(data, serializerSettings);
            HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, serializerSettings));
        }

        public Task<TResult> PutAsync<TResult>(string uri, TResult data,string token="")
        {
            return PutAsync<TResult, TResult>(uri, data,token);
        }


        public async Task<TResult> PutAsync<TRequest,TResult>(string uri, TRequest data, string token="")
        {
            HttpClient httpClient = CreateHttpClient(token);
            var serialized = await Task.Run(() => JsonConvert.SerializeObject(data, serializerSettings));
            HttpResponseMessage response = await httpClient.PutAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, serializerSettings));
        }

        public async Task<TResult> DeleteAsync<TResult>(string uri, string token ="")
        {
            HttpClient httpClient = CreateHttpClient(token);
            HttpResponseMessage response =  await httpClient.DeleteAsync(uri);

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, serializerSettings));
        }

        HttpClient CreateHttpClient(string token="")
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new ConnectivityException();
            }

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        }

       async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}

