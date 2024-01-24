namespace Gopet.Data.Dialog
{
    public class MessageItem
    {
        public int msgId;
        public int fromID;
        public string fromName;
        public string message;
        public long sentTime;
        public sbyte type = 0;
        public bool isRead;
    }
}