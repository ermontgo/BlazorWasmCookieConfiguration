using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWasmCookieConfiguration
{
    public class CookieConfigurationProvider : ConfigurationProvider
    {
        private readonly string cookieName;
        private readonly IJSInProcessRuntime runtime;

        public CookieConfigurationProvider(string cookieName, IJSInProcessRuntime runtime)
        {
            this.cookieName = cookieName;
            this.runtime = runtime;
        }

        public override void Load()
        {
            try
            {
                var cookieValue = runtime.Invoke<string>("cookieConfiguration.getCookie", cookieName);
                if (!string.IsNullOrWhiteSpace(cookieValue))
                {
                    var tree = JObject.Parse(cookieValue);
                    var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    TraverseTree(tree, data, cookieName);

                    Data = data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TraverseTree(JObject tree, Dictionary<string, string> data, string parentName)
        {
            foreach (var leaf in tree)
            {
                if (leaf.Value.Type == JTokenType.Object)
                {
                    TraverseTree((JObject)leaf.Value, data, ConfigurationPath.Combine(parentName, leaf.Key));
                }
                else if (leaf.Value.Type == JTokenType.Array)
                {
                    int idx = 0;
                    foreach (var item in leaf.Value)
                    {
                        TraverseTree((JObject)item, data, ConfigurationPath.Combine(parentName, leaf.Key, idx.ToString()));
                    }
                }
                else
                {
                    data.Add(ConfigurationPath.Combine(parentName, leaf.Key), leaf.Value.ToObject<string>());
                }
            }
        }
    }
}
