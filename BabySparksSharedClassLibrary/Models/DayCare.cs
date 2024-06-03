using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    public class DayCare : User
    {
        public string Address { get; set; }
        public string City { get; set; }   
        public string PinCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
