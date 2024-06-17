using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    [FirestoreData]
    public class Nanny : User
    {
        [FirestoreProperty]
        public string Bio { get; set; }
        [FirestoreProperty]
        public float Experience {  get; set; }
        [FirestoreProperty]
        public float HourlyCharges { get; set; }
        [FirestoreProperty]
        public float Ratings { get; set; }
    }
}
