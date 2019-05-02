using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface IAuthorizeApi
    {
        Task Login(string username, string password);
        Task Register(string username, string password);
        Task Logout();
    }
}
