using Proxies.Configs;
using RestSharp;

namespace Proxies
{
    public class PerNoelAppProxy : ProxyBase
    {
        private const string AppName = "perNoelApp";
        
        public PerNoelAppProxy(IRestClient restClient, ProxySettingCollection settingCollection) 
            : base(restClient, settingCollection, AppName)
        {

        }
    }
}
