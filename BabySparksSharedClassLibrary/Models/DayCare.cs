﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    public class DayCare : User
    {
        public string DayCareName { get; set; }
        public string Description { get; set; }
        public string ServiceProvided { get; set; }
    }
}
