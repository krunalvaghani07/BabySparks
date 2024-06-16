using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    public class Nanny : User
    {
        public string Bio { get; set; }
        public float Experience {  get; set; }
        public float HourlyCharges { get; set; }
    }
}
