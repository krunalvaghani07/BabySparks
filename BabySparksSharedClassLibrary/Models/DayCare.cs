using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    [FirestoreData]
    public class DayCare : User
    {
        [FirestoreProperty]
        public string DayCareName { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public string ServiceProvided { get; set; }
        [FirestoreProperty]
        public float Ratings { get; set; }
    }
}
