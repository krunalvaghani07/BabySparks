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
    public class User
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [FirestoreProperty]
        public string Address { get; set; }
        [FirestoreProperty]
        public string City { get; set; }
        [FirestoreProperty]
        public string PinCode { get; set; }
        
        [FirestoreProperty]
        public string PhoneNumber { get; set; }
        public string ProfileImageUrl { get; set; }
        [FirestoreProperty]
        public UserType userType { get; set; }

    }
}
