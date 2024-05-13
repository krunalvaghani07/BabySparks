using BabySparksSharedClassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    public class Child : User
    {
        public string Age {  get; set; }
        public Gender Gender { get; set; }
    }
}
