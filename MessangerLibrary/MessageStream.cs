using System.IO;

namespace MessangerLibrary
{
    public class MessageStream
    {
        // create new Message
        static public Message CreateMessage(StatusMessage typeMesage, string username, string content) => new Message()
        {
            TypeMessage = typeMesage,
            Username = username,
            Content = content
        };

        // transform message in bytes array and return it
        static public byte[] WriteMessage(Message message)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    writer.Write((int)message.TypeMessage);
                    writer.Write(message.Username);
                    writer.Write(message.Content);
                }
                return memoryStream.ToArray();
            }
        }

        // transform bytes array in message and return it
        static public Message ReadMessage(byte[] byteMessage)
        {
            Message message = new Message();

            using (MemoryStream memoryStream = new MemoryStream(byteMessage))
            {
                using (BinaryReader reader = new BinaryReader(memoryStream))
                {
                    message.TypeMessage = (StatusMessage)reader.ReadInt32();
                    message.Username = reader.ReadString();
                    message.Content = reader.ReadString();
                }
            }

            return message;
        }
    }
}
