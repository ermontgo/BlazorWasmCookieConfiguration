using BlazorWasmCookieConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    public static class CookieConfigurationExtensions
    {
        public static IConfigurationBuilder AddCookieConfiguration(this IConfigurationBuilder builder, CookieConfigurationOptions options)
        {
            builder.Add(new CookieConfigurationSource(options));
            return builder;
        }

        public static IConfigurationBuilder AddCookieConfiguration(this IConfigurationBuilder builder, string cookieName, IJSRuntime runtime)
        {
            return AddCookieConfiguration(builder, new CookieConfigurationOptions(cookieName, runtime));
        }
    }
}
