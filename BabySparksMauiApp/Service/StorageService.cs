using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksMauiApp.Service
{
    public class StorageService : IStorageService
    {
        public void ClearAll()
        {
            Preferences.Clear();
        }

        public void Delete(string key)
        {
            Preferences.Remove(key);
        }

        public Task<bool> Exists(string key)
        {
            return Task.FromResult(Preferences.ContainsKey(key));
        }

        public async Task<T> GetValue<T>(string key)
        {
            T UnpackedValue = default;
            string keyvalue = Preferences.Get(key, string.Empty);

            if (keyvalue != null && !string.IsNullOrEmpty(keyvalue))
            {
                UnpackedValue = JsonConvert.DeserializeObject<T>(keyvalue);
            }
            return UnpackedValue;
        }

        public void SetValue(string key, object value)
        {
            string keyvalue = JsonConvert.SerializeObject(value);
            if (keyvalue != null && !string.IsNullOrEmpty(keyvalue))
            {
                Preferences.Set(key, keyvalue);
            }
        }
    }
}
