using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerLibrary
{
    public enum StatusMessage
    {
        LogIn, //if log in messanger
        SigIn, //if sig in messanger
        Forgaut, // if forgaut password
        ForgoutCheck, // if chek code for forgaut pasword
        Joined, // if joined in chat
        Message, // if sednd message
        Detached, // if detached in chat
        Null // null :)
    }

    public class Message
    {
        public StatusMessage TypeMessage { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }

        public override string ToString() => $"{Username}:\n{Content}";
    }
}
