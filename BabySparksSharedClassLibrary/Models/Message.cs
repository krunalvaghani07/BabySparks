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
    public class Message
    {
        public string Id { get; set; }
        [FirestoreProperty]
        public string RecieverId { get; set; }
        [FirestoreProperty]
        public string FromId { get; set; }
        [FirestoreProperty]
        public string FromName { get; set; }
        [FirestoreProperty]
        public string Content { get; set; }
        [FirestoreProperty]
        public DateTime Timestamp {  get; set; }
    }
}
