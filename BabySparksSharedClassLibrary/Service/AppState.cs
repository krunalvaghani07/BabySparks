using BabySparksSharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.ServiceProvider
{
    public class AppState
    {
        public bool IsWeb { get; set; }
        public bool IsDesktop { get; set; }
        
        public bool IsAuthenticated {  get; set; }
        public User user { get; set; }
    }
}
