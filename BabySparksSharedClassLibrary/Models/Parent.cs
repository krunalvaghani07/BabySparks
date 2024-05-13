using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    public class Parent : User
    {
         public List<Child> Children { get; set; }
    }
}
