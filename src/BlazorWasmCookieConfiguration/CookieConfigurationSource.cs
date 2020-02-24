using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWasmCookieConfiguration
{
    class CookieConfigurationSource : IConfigurationSource
    {
        private readonly CookieConfigurationOptions options;

        public CookieConfigurationSource(CookieConfigurationOptions options)
        {
            this.options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CookieConfigurationProvider(options.CookieName, options.JSRuntime);
        }
    }
}
