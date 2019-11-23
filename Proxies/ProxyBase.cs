using System;
using System.Net;
using Proxies.Configs;
using Proxies.Extensions;
using Proxies.Models;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Proxies
{
    public abstract class ProxyBase: IBehaviorProxy
    {
        private readonly IRestClient restClient;
        private readonly ProxySettings proxySettings;
        
        protected ProxyBase(IRestClient restClient, ProxySettingCollection settingCollection, string appName)
        {
            
            proxySettings = settingCollection.GetByName(appName);
         
            this.restClient = restClient;
            this.restClient.BaseUrl = new Uri(proxySettings.Url);
        }
        
        public VerdictResult GetVerdict(string fio, int age)
        {
            var request = new RestRequest("api/BoysAndGirls");
            request.AddParameter("fio", fio);
            request.AddParameter("age", age);

            var response = restClient.Execute(request);

            if(response.ErrorException != null)
                throw new Exception("При выполнении запроса произошла ошибка", response.ErrorException);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new VerdictResult()
                {
                    StatusCode = response.StatusCode,
                    ErrorMessage = response.Content
                };
            }
            
            var verdictDto = JsonSerializer.Deserialize<VerdictDto>(response.Content);
            return new VerdictResult()
            {
                VerdictDto = verdictDto,
                StatusCode = response.StatusCode,
                ErrorMessage = null
            };;
        }
        
        private bool TryGetErrorMessage(IRestResponse response, out string errorMessage)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                errorMessage = response.Content;
                return true;
            }

            errorMessage = null;
            return false;
        }
    }
}
