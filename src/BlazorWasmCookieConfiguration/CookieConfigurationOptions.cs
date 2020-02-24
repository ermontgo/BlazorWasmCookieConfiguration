using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWasmCookieConfiguration
{
    public class CookieConfigurationOptions
    {
        public string CookieName { get; set; }
        public IJSInProcessRuntime JSRuntime { get; set; }

        public CookieConfigurationOptions(string cookieName, IJSRuntime runtime)
        {
            CookieName = cookieName;

            if (runtime is IJSInProcessRuntime clientRuntime)
            {
                JSRuntime = clientRuntime;
            }
            else throw new ArgumentException("CookieConfigurationProvider can only be used in client side Blazor applications. Please make sure the IJSRuntime object implements IJSInProcessRuntime", nameof(runtime));

        }
    }
}
