namespace MessangerLibrary.Chat
{
    public enum StatusMessage
    {
        LogIn, //log in to messanger
        SigIn, // sig in to messanger
        GenereteNewPassword, // get new password
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
