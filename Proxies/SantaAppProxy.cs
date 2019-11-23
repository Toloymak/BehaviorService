using Proxies.Configs;
using RestSharp;

namespace Proxies
{
    public class SantaAppProxy : ProxyBase
    {
        private const string AppName = "santaApp";
        
        public SantaAppProxy(IRestClient restClient, ProxySettingCollection settingCollection) 
            : base(restClient, settingCollection, AppName)
        {

        }
    }
}
