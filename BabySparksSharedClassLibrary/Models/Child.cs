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
    public class Child : User
    {
        public int Age {  get; set; }
        [FirestoreProperty]
        public Gender Gender { get; set; }
        [FirestoreProperty]
        public int height { get; set; }
        [FirestoreProperty]
        public int weight { get; set; }
        [FirestoreProperty]
        public DateTime DateOfBirth { get; set; } = DateTime.Today.Date;
        [FirestoreProperty]
        public DateTime createdRecord { get; set; }
        [FirestoreProperty]
        public DateTime modifiedRecord { get; set; } = DateTime.Today.Date;
    }
}
