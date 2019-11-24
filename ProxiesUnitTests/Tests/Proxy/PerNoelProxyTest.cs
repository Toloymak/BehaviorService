using Proxies;
using Proxies.Configs;
using RestSharp;

namespace UnitTests.Tests.Proxy
{
    public class PerNoelProxyTest : ProxyTestsBase
    {
        public PerNoelProxyTest() : base("perNoelApp")
        {
        }

        protected override IBehaviorProxy CreateProxy(IRestClient restClient, 
                                                      ProxySettingCollection settingCollection) => 
            new PerNoelAppProxy(restClient, settingCollection);
    }
}