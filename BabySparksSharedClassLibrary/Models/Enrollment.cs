using BabySparksSharedClassLibrary.Enums;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    [FirestoreData]
    public class Enrollment
    {
        public string DocId { get; set; }
        [FirestoreProperty]
        public string ChildId { get; set; }
        [FirestoreProperty]
        public string ParentId { get; set; }
        [FirestoreProperty]
        public DateTime EnrollmentDate { get; set; } = DateTime.Today.Date;
    }
}
