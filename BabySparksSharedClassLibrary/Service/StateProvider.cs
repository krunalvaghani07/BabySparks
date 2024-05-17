using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.ServiceProvider
{
    public class StateProvider : AuthenticationStateProvider
    {
        private readonly IStorageService _localStorage;

        public StateProvider(IStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                if (_localStorage.Exists("user"))
                {
                    var _userId =  _localStorage.GetValue<User>("user");

                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "Basic User")
                        };
                    identity = new ClaimsIdentity(claims, "authentication");
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));

                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            AuthenticationState authState = new AuthenticationState(new ClaimsPrincipal(identity));
            return authState;
        }
        public void ManageUser()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
