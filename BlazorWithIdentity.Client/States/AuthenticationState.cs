using BlazorWithIdentity.Client.Services.Contracts;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.States
{
    public class AuthenticationState
    {
        private readonly IAuthorizeApi _authorizeApi;
        private readonly IJSRuntime _jsRuntime;

        public string UserName { get; protected set; }

        public AuthenticationState(IAuthorizeApi authorizeApi, IJSRuntime jsRuntime)
        {
            _authorizeApi = authorizeApi;
            _jsRuntime = jsRuntime;
        }

        public Task<bool> IsLoggedIn()
        {
            return _jsRuntime.InvokeAsync<bool>("Authorization_LoginCookieExists");
        }

        public async Task Login(string username, string password)
        {
            await _authorizeApi.Login(username, password);
            UserName = username;
        }

        public async Task Register(string username, string password)
        {
            await _authorizeApi.Register(username, password);
            UserName = username;
        }

        public async Task Logout()
        {
            await _authorizeApi.Logout();
            UserName = null;
        }

    }
}
