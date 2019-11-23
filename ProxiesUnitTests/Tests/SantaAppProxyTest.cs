using Proxies;
using Proxies.Configs;
using RestSharp;

namespace UnitTests.Tests
{
    public class SantaAppProxyTest: ProxyTestsBase
    {
        public SantaAppProxyTest() : base("santaApp")
        {
        }

        protected override IBehaviorProxy CreateProxy(IRestClient restClient, 
            ProxySettingCollection settingCollection) => 
            new SantaAppProxy(restClient, settingCollection);
    }
}