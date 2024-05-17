using BabySparksSharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.IServices
{
    public interface IStorageService
    {
        public void SetValue(string key, object value);

        public T GetValue<T> (string key);
        public void Delete(string key);
        public bool Exists(string key);
        public void ClearAll();

    }
}
