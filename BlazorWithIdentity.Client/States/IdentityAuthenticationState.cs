using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.States
{
    public class IdentityAuthenticationState
    {
        private readonly IAuthorizeApi _authorizeApi;
        private UserInfo userInfo;
        public event Action<UserInfo> UserInfoChanged;

        public IdentityAuthenticationState(IAuthorizeApi authorizeApi)
        {
            _authorizeApi = authorizeApi;
        }

        protected void NotifyAuthenticationStateChanged()
        {
            UserInfoChanged?.Invoke(userInfo);
        }

        public async Task Login(LoginParameters loginParameters)
        {
            userInfo = await _authorizeApi.Login(loginParameters);
            NotifyAuthenticationStateChanged();
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            userInfo = await _authorizeApi.Register(registerParameters);
            NotifyAuthenticationStateChanged();
        }

        public async Task Logout()
        {
            await _authorizeApi.Logout();
            userInfo = null;
            NotifyAuthenticationStateChanged();
        }

        public async Task<UserInfo> GetUserInfo()
        {
            if (userInfo != null && userInfo.IsAuthenticated) return userInfo;
            userInfo = await _authorizeApi.GetUserInfo();
            return userInfo;
        }

    }
}
