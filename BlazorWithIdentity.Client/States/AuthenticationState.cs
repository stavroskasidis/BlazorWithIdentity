using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared;
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

        public async Task Login(LoginParameters loginParameters)
        {
            await _authorizeApi.Login(loginParameters);
            UserName = loginParameters.Username;
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            await _authorizeApi.Register(registerParameters);
            UserName = registerParameters.Username;
        }

        public async Task Logout()
        {
            await _authorizeApi.Logout();
            UserName = null;
        }

    }
}
