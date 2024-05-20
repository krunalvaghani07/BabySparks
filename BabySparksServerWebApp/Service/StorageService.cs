using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;

namespace BabySparksServerWebApp.Service
{
    public class StorageService : IStorageService
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly IServiceProvider _serviceProvider;

        public StorageService(IServiceProvider serviceProvider, ProtectedSessionStorage sessionStorage)
        {
            _serviceProvider = serviceProvider;
            _sessionStorage = sessionStorage;
        }
        public void ClearAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var localStorageService = scope.ServiceProvider.GetRequiredService<ILocalStorageService>();
                Task.Run(async () => await localStorageService.ClearAsync());
            }
        }

        public async void Delete(string key)
        {
            await _sessionStorage.DeleteAsync(key);
        }

        public async Task<bool> Exists(string key)
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<User>(key);
            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
            if (userSession == null)
                return false;
            else return true;
        }

        public async Task<T> GetValue<T>(string key)
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<T>(key);
            if (userSessionStorageResult.Success)
            {
                return userSessionStorageResult.Value;
            }

            return default(T);
        }

        public async void SetValue(string key, object value)
        {
            await _sessionStorage.SetAsync(key, value);
        }
    }
}
