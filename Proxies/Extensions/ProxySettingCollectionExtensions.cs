using System;
using System.Linq;
using Proxies.Configs;

namespace Proxies.Extensions
{
    public static class ProxySettingCollectionExtensions
    {
        public static ProxySettings GetByName(this ProxySettingCollection collection, string appName)
        {
            if (collection == null || collection.Proxies.Length == 0)
                throw new ArgumentException($"Настройки для приложений не найдены");

            var proxySettings = collection.Proxies
                .FirstOrDefault(p => string.Equals(
                    p.Name,
                    appName,
                    StringComparison.CurrentCultureIgnoreCase));

            if (proxySettings == null)
                throw new ArgumentException($"Настройки для приложения {appName} не обнаруженны");

            return proxySettings;
        }
            
    }
}