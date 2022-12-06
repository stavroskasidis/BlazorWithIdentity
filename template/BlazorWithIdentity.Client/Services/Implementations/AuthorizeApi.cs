using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Implementations;

public class AuthorizeApi : IAuthorizeApi
{
    private readonly HttpClient _httpClient;

    public AuthorizeApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Login(LoginParameters loginParameters)
    {
        //var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsJsonAsync("api/Authorize/Login", loginParameters);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task Logout()
    {
        var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
        result.EnsureSuccessStatusCode();
    }

    public async Task Register(RegisterParameters registerParameters)
    {
        //var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsJsonAsync("api/Authorize/Register", registerParameters);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task<UserInfo> GetUserInfo()
    {
        var result = await _httpClient.GetFromJsonAsync<UserInfo>("api/Authorize/UserInfo");
        return result;
    }
}
