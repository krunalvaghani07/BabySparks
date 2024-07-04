using BabySparksSharedClassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Models
{
    public class Message
    {
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp {  get; set; }
        public UserType FromMessageUserType { get; set; }

    }
}
